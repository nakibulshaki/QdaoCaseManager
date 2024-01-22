using QdaoCaseManager.Dtos;
using QdaoCaseManager.Extra;
using QdaoCaseManager.Shared.Dtos;

namespace QdaoCaseManager.Repositories.Notes
{
    public interface INoteRepository
    {
        Task<PaginatedList<NoteDto>> GetNotesWithPaginationAsync(FilterNoteDto filterNoteDto);
        Task CreateNote(CreateUpdateNoteDto noteDto);
        Task<bool> UpdateNoteAsync(CreateUpdateNoteDto updatedNoteDto);
        Task<bool> DeleteNoteAsync(int noteId);
        Task<CreateUpdateNoteDto> GetNoteByIdAsync(int noteId);
    }
}
