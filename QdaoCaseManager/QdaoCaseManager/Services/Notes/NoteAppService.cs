using Microsoft.EntityFrameworkCore;
using QdaoCaseManager.Data;
using QdaoCaseManager.Shared.Entites;

namespace QdaoCaseManager.Services.Notes;
public class NoteAppService : INoteAppService
{
    private readonly ApplicationDbContext _dbContext;

    public NoteAppService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
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
}

