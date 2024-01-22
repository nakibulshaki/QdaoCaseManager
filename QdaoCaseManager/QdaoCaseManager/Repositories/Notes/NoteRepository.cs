
using Microsoft.Data.SqlClient;
using QdaoCaseManager.Extra;
using QdaoCaseManager.Shared.Dtos;
using QdaoCaseManager.Shared.Entites;
using System.Data;
namespace QdaoCaseManager.Repositories.Notes;
public class NoteRepository:INoteRepository
{
    private readonly IConfiguration _config;

    public NoteRepository(IConfiguration config)
    {
       _config = config;
    }

    public async Task<PaginatedList<NoteDto>> GetNotesWithPaginationAsync(FilterNoteDto filterNoteDto)
    {
        int totalCount = 0;

        // You can use _context.Database.GetDbConnection() to get the underlying SqlConnection
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

                // Execute the stored procedure and retrieve results
                using (var reader = await command.ExecuteReaderAsync())
                {

                    // Check if the output parameter is present
                    if (totalCountParam.Value is not null)
                    {
                        totalCount = (int)totalCountParam.Value;
                    }

                    var noteDtos = MapToNoteDtoList(reader);

                    // Extract total count after execution
                    return new PaginatedList<NoteDto>(noteDtos, filterNoteDto.CurrentPage, filterNoteDto.PageSize, totalCount);
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
   
}