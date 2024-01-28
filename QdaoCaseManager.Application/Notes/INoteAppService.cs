
using QdaoCaseManager.DTOs.Common.Models;
using QdaoCaseManager.DTOs.Notes;

namespace QdaoCaseManager.Services.Notes;
public interface INoteAppService
{
    Task CreateNote(CreateUpdateNoteDto note);
    Task<CreateUpdateNoteDto> GetNoteById(int id);
    Task UpdateNote(int id, CreateUpdateNoteDto updatedNote);
    Task DeleteNote(int id);
    IList<SelectItem> GetNoteCases();
    Task<PaginatedList<NoteDto>> GetFiltedNotes(FilterNoteDto filter);
}

