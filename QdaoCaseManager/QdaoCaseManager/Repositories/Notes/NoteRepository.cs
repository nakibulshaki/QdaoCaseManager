
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

    public async Task<PaginatedList<NoteDto>> GetNotesWithPaginationAsync(FilterNoteDto filterNoteDto)
    {
        int totalCount = 0;

        using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
        {
            await connection.OpenAsync();

            using (var command = new SqlCommand("GetNotesWithPagination", connection))
            {
                var totalCountParam = new SqlParameter("@TotalCount", totalCount)
                {
                    Direction = ParameterDirection.Output,
                    SqlDbType = SqlDbType.Int
                };
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@CurrentPage", filterNoteDto.CurrentPage));
                command.Parameters.Add(new SqlParameter("@PageSize", filterNoteDto.PageSize));
                command.Parameters.Add(new SqlParameter("@SearchString", (object)filterNoteDto.SearchString ?? DBNull.Value));
                command.Parameters.Add(totalCountParam);

                using (var reader = await command.ExecuteReaderAsync())
                {

                    // Check if the output parameter is present
                    if (totalCountParam.Value is not null)
                    {
                        totalCount = (int)totalCountParam.Value;
                    }

                    var noteDtos = MapToNoteDtoList(reader);

                    return new PaginatedList<NoteDto>
                    (noteDtos,
                    filterNoteDto.CurrentPage,
                    filterNoteDto.PageSize,
                    totalCount);
                }
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