using QdaoCaseManager.DTOs.Common.Models;
using QdaoCaseManager.DTOs.Notes;

namespace QdaoCaseManager.Domain.Repositories;
public interface INoteRepository
{
    Task<PaginatedList<NoteDto>> GetNotesWithPaginationAsync(FilterNoteDto filterNoteDto);
    Task CreateNote(CreateUpdateNoteDto noteDto);
    Task<bool> UpdateNoteAsync(CreateUpdateNoteDto updatedNoteDto);
    Task<bool> DeleteNoteAsync(int noteId);
    Task<CreateUpdateNoteDto> GetNoteByIdAsync(int noteId);
}

