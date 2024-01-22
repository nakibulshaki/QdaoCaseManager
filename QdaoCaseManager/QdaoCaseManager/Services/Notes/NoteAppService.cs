using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QdaoCaseManager.Data;
using QdaoCaseManager.Extra;
using QdaoCaseManager.Repositories.Notes;
using QdaoCaseManager.Shared.Dtos;
using QdaoCaseManager.Shared.Dtos.Cases;
using QdaoCaseManager.Shared.Entites;

namespace QdaoCaseManager.Services.Notes;
public class NoteAppService : INoteAppService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly INoteRepository _noteRepository;

    public NoteAppService(ApplicationDbContext dbContext,
    INoteRepository noteRepository)
    {
        _dbContext = dbContext;
        _noteRepository = noteRepository;
    }
    public async Task<Note> CreateNote(Note note)
    {
        _dbContext.Notes.Add(note);
        await _dbContext.SaveChangesAsync();
        return note;
    }
    public async Task<IEnumerable<Note>> GetNotes()
    {
        return await _dbContext.Notes.ToListAsync();
    }
    
    public async Task<PaginatedList<NoteDto>> GetFiltedNotes(FilterNoteDto filter)
    {
        var result = await _noteRepository.GetNotesWithPaginationAsync(filter);
        return result;
    }

    public async Task<Note> GetNoteById(int id)
    {
        var note = await _dbContext.Notes.FindAsync(id);
        return note;
    }
    public async Task<Note> UpdateNote(int id, Note updatedNote)
    {
        var note = await GetNoteById(id);
        if (note == null)
        {
            throw new InvalidOperationException("Note not found");
        }

        _dbContext.Entry(note).CurrentValues.SetValues(updatedNote);
        await _dbContext.SaveChangesAsync();
        return note;
    }
    public async Task DeleteNote(int id)
    {
        var note = await GetNoteById(id);
        if (note == null)
        {
            throw new InvalidOperationException("Note not found");
        }

        _dbContext.Notes.Remove(note);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IList<SelectListItem>> GetNoteCases()
    {
        var caseUsers = await _dbContext.Cases
                                        .Select(x => new SelectListItem
                                        {
                                            Value = x.Id.ToString(),
                                            Text = x.Tittle
                                        })
                                        .ToListAsync();

        return caseUsers;
    }
}

