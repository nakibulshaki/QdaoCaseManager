
using QdaoCaseManager.Domain.Entities;
using QdaoCaseManager.Domain.Repositories;
using QdaoCaseManager.DTOs.Common.Models;
using QdaoCaseManager.DTOs.Notes;

namespace QdaoCaseManager.Services.Notes;
public class NoteAppService : INoteAppService
{
    private readonly INoteRepository _noteRepository;
    private readonly IRepository<Case> _caseRepository;
    public NoteAppService(INoteRepository noteRepository,
        IRepository<Case> caseRepository)
    {
        _noteRepository = noteRepository;
        _caseRepository = caseRepository;
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

    public IList<SelectItem> GetNoteCases()
    {
        var query = _caseRepository.AsQueryable();
    var caseUsers = query
                    .Select(x => new SelectItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Tittle
                    })
                    .ToList();

        return caseUsers;
    }
}

