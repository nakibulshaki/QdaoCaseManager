
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using QdaoCaseManager.Domain.Entities;
using QdaoCaseManager.Domain.Repositories;
using QdaoCaseManager.DTOs.Common.Models;
using QdaoCaseManager.DTOs.Notes;
using System.Data;
namespace QdaoCaseManager.Infrastructure.Repositories;
public class NoteRepository : INoteRepository
{
    private readonly IConfiguration _config;

    public NoteRepository(IConfiguration config)
    {
        _config = config;
    }

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
                Created = reader.GetDateTimeOffset(reader.GetOrdinal("Created")),
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