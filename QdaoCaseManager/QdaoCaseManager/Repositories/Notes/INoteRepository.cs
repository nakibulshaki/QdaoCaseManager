using QdaoCaseManager.Extra;
using QdaoCaseManager.Shared.Dtos;

namespace QdaoCaseManager.Repositories.Notes
{
    public interface INoteRepository
    {
        Task<PaginatedList<NoteDto>> GetNotesWithPaginationAsync(FilterNoteDto filterNoteDto);
    }
}
