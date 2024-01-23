﻿
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.Data.SqlClient;
using NuGet.Versioning;
using QdaoCaseManager.Dtos;
using QdaoCaseManager.Extra;
using QdaoCaseManager.Shared.Dtos;
using QdaoCaseManager.Shared.Entites;
using SendGrid.Helpers.Mail;
using System.Data;
namespace QdaoCaseManager.Repositories.Notes;
public class NoteRepository : INoteRepository
{
    private readonly IConfiguration _config;

    public NoteRepository(IConfiguration config)
    {
        _config = config;
    }
    //public JsonResult AjaxMethod(int pageIndex, string searchTerm)
    //{
    //    CustomerModel model = new CustomerModel();
    //    model.SearchTerm = searchTerm;
    //    model.PageIndex = pageIndex;
    //    model.PageSize = 10;

    //    List<Customer> customers = new List<Customer>();
    //    string constring = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    //    using (SqlConnection con = new SqlConnection(constring))
    //    {
    //        using (SqlCommand cmd = new SqlCommand("GetCustomersPageWise", con))
    //        {
    //            cmd.CommandType = CommandType.StoredProcedure;
    //            cmd.Parameters.AddWithValue("@SearchTerm", model.SearchTerm);
    //            cmd.Parameters.AddWithValue("@PageIndex", model.PageIndex);
    //            cmd.Parameters.AddWithValue("@PageSize", model.PageSize);
    //            cmd.Parameters.Add("@RecordCount", SqlDbType.VarChar, 30);
    //            cmd.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
    //            con.Open();
    //            SqlDataReader sdr = cmd.ExecuteReader();
    //            while (sdr.Read())
    //            {
    //                customers.Add(new Customer
    //                {
    //                    CustomerID = sdr["CustomerID"].ToString(),
    //                    ContactName = sdr["ContactName"].ToString(),
    //                    City = sdr["City"].ToString(),
    //                    Country = sdr["Country"].ToString()
    //                });
    //            }
    //            con.Close();

    //            model.Customers = customers;
    //            model.RecordCount = Convert.ToInt32(cmd.Parameters["@RecordCount"].Value);
    //        }
    //    }

    //    return Json(model);
    //}
    public async Task<PaginatedList<NoteDto>> GetNotesWithPaginationAsync(FilterNoteDto filterNoteDto)
    {

        using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
        {
            await connection.OpenAsync();

            using (var command = new SqlCommand("GetNotesWithPagination", connection))
            {
                var totalCountParam = new SqlParameter("@TotalCount", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output,
                };
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@CurrentPage", filterNoteDto.CurrentPage));
                command.Parameters.Add(new SqlParameter("@PageSize", filterNoteDto.PageSize));
                command.Parameters.Add(new SqlParameter("@SearchString", (object)filterNoteDto.SearchString ?? DBNull.Value));
                command.Parameters.Add(totalCountParam);
                List<NoteDto> noteDtos = new List<NoteDto>();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    noteDtos = MapToNoteDtoList(reader);
                }
                return new PaginatedList<NoteDto>
                    (noteDtos,
                    filterNoteDto.CurrentPage,
                    filterNoteDto.PageSize,
                    Convert.ToInt32(totalCountParam.Value));
            }
        }
        
    }

    private List<NoteDto> MapToNoteDtoList(SqlDataReader reader)
    {
        var noteDtos = new List<NoteDto>();

        while (reader.Read())
        {
            var noteDto = new NoteDto
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Content = reader.GetString(reader.GetOrdinal("Content")),
                CaseTittle = reader.GetString(reader.GetOrdinal("CaseTittle")),
                CreateDate = reader.GetDateTime(reader.GetOrdinal("CreateDate"))
            };

            noteDtos.Add(noteDto);
        }

        return noteDtos;
    }

    public async Task CreateNote(CreateUpdateNoteDto noteDto)
    {
        using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
        {
            await connection.OpenAsync();

            using (var command = new SqlCommand("CreateNote", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Content", noteDto.Content);
                command.Parameters.AddWithValue("@CaseId", noteDto.CaseId);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected < 1)
                {
                    throw new Exception("No row added to Notes in database");
                }
            }
        }
    }
    public async Task<bool> UpdateNoteAsync(CreateUpdateNoteDto updatedNoteDto)
    {
        using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
        {
            await connection.OpenAsync();

            using (var command = new SqlCommand("UpdateNote", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@Id", updatedNoteDto.Id));
                command.Parameters.Add(new SqlParameter("@Content", updatedNoteDto.Content));
                command.Parameters.Add(new SqlParameter("@CaseId", updatedNoteDto.CaseId));

                int rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected < 1)
                {
                    throw new Exception("No row added to Notes in database");
                }
                return rowsAffected > 0;
            }
        }

    }
    public async Task<bool> DeleteNoteAsync(int noteId)
    {
        using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
        {
            await connection.OpenAsync();

            using (var command = new SqlCommand("DeleteNote", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@Id", noteId));

                int rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected < 1)
                {
                    throw new Exception("No row added to Notes in database");
                }
                return rowsAffected > 0;
            }
        }
    }
    public async Task<CreateUpdateNoteDto> GetNoteByIdAsync(int noteId)
    {
        using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
        {
            await connection.OpenAsync();

            using (var command = new SqlCommand("GetNoteById", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@Id", noteId));

                using var reader = await command.ExecuteReaderAsync();
                var noteDtos = new List<CreateUpdateNoteDto>();

                while (reader.Read())
                {
                    var noteDto = new CreateUpdateNoteDto
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Content = reader.GetString(reader.GetOrdinal("Content")),
                        CaseId = reader.GetInt32(reader.GetOrdinal("CaseId")),
                    };

                    noteDtos.Add(noteDto);
                }

                return noteDtos
                      .FirstOrDefault();
            }
        }
    }
}