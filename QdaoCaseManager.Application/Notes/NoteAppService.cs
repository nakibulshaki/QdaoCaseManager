using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QdaoCaseManager.Infrastructure.Data;
using QdaoCaseManager.Dtos;
using QdaoCaseManager.Extra;
using QdaoCaseManager.Repositories.Notes;
using QdaoCaseManager.Shared.Dtos;
using QdaoCaseManager.Domain.Entities;

namespace QdaoCaseManager.Services.Notes;
public class NoteAppService : INoteAppService
{
    private readonly INoteRepository _noteRepository;
    private readonly ApplicationDbContext _dbContext;
    public NoteAppService(INoteRepository noteRepository,
           ApplicationDbContext dbContext)
    {
        _noteRepository = noteRepository;
        _dbContext = dbContext;
    }
    public async Task CreateNote(CreateUpdateNoteDto note)
    {
       await _noteRepository.CreateNote(note);
    }
    
    public async Task<PaginatedList<NoteDto>> GetFiltedNotes(FilterNoteDto filter)
    {
        var result = await _noteRepository.GetNotesWithPaginationAsync(filter);
        return result;
    }

    public async Task<CreateUpdateNoteDto> GetNoteById(int id)
    {
        var note = await _noteRepository.GetNoteByIdAsync(id);
        return note;
    }
    public async Task UpdateNote(int id, CreateUpdateNoteDto noteDto)
    {
        var result = await _noteRepository.UpdateNoteAsync(noteDto);
        if (!result)
            throw new InvalidOperationException("Note not found");
    }
    public async Task DeleteNote(int id)
    {
        var result = await _noteRepository.DeleteNoteAsync(id);
        if (!result)
            throw new InvalidOperationException("Note not found");
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

