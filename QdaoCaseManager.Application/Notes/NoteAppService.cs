
using QdaoCaseManager.Domain.Entities;
using QdaoCaseManager.Domain.Repositories;
using QdaoCaseManager.DTOs.Common.Models;
using QdaoCaseManager.DTOs.Notes;

namespace QdaoCaseManager.Services.Notes;
public class NoteAppService : INoteAppService
{
    private readonly INoteRepository _noteRepository;
    private readonly ICaseRepository _caseRepository;
    public NoteAppService(INoteRepository noteRepository,
        ICaseRepository caseRepository)
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

    public async Task<IList<SelectItem>> GetNoteCases()
    {
        return await _caseRepository.GetCaseUsers();
    }
}

