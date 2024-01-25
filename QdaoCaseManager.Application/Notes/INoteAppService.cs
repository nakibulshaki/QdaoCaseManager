using QdaoCaseManager.Application.Common.Models;
using QdaoCaseManager.Application.Notes.Dtos;

namespace QdaoCaseManager.Services.Notes;
public interface INoteAppService
{
    Task CreateNote(CreateUpdateNoteDto note);
    Task<CreateUpdateNoteDto> GetNoteById(int id);
    Task UpdateNote(int id, CreateUpdateNoteDto updatedNote);
    Task DeleteNote(int id);
    Task<IList<SelectItem>> GetNoteCases();
    Task<PaginatedList<NoteDto>> GetFiltedNotes(FilterNoteDto filter);
}

