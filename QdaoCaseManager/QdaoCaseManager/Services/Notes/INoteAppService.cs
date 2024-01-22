
using Microsoft.AspNetCore.Mvc.Rendering;
using QdaoCaseManager.Dtos;
using QdaoCaseManager.Extra;
using QdaoCaseManager.Shared.Dtos;
using QdaoCaseManager.Shared.Entites;

namespace QdaoCaseManager.Services.Notes;
public interface INoteAppService
{
    Task CreateNote(CreateUpdateNoteDto note);
    Task<CreateUpdateNoteDto> GetNoteById(int id);
    Task UpdateNote(int id, CreateUpdateNoteDto updatedNote);
    Task DeleteNote(int id);
    Task<IList<SelectListItem>> GetNoteCases();
    Task<PaginatedList<NoteDto>> GetFiltedNotes(FilterNoteDto filter);
}

