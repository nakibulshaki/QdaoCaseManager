
using Microsoft.AspNetCore.Mvc.Rendering;
using QdaoCaseManager.Shared.Entites;

namespace QdaoCaseManager.Services.Notes;
public interface INoteAppService
{
    Task<Note> CreateNote(Note note);
    Task<IEnumerable<Note>> GetNotes();
    Task<Note> GetNoteById(int id);
    Task<Note> UpdateNote(int id, Note updatedNote);
    Task DeleteNote(int id);
    Task<IList<SelectListItem>> GetNoteCases();
}

