﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QdaoCaseManager.Services.Notes;
using QdaoCaseManager.Domain.Entities;
using QdaoCaseManager.DTOs.Common.Models;
using QdaoCaseManager.DTOs.Notes;
namespace QdaoCaseManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotesController : ControllerBase
{
    private readonly INoteAppService _noteAppService;

    public NotesController(INoteAppService noteAppService)
    {
        _noteAppService = noteAppService;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedList<NoteDto>>> GetNote([FromQuery] FilterNoteDto filter)
    {
        var notes = await _noteAppService.GetFiltedNotes(filter);
        if (notes != null)
            return Ok(notes);
        else
            return NotFound();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CreateUpdateNoteDto>> GetNote(int id)
    {
        var note = await _noteAppService.GetNoteById(id);

        if (note == null)
        {
            return NotFound();
        }

        return note;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutNote(int id, CreateUpdateNoteDto note)
    {
        await _noteAppService.UpdateNote(id, note);
        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<Note>> PostNote(CreateUpdateNoteDto note)
    {
        await _noteAppService.CreateNote(note);
        return CreatedAtAction("GetNote", new { id = note.Id }, note);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNote(int id)
    {
        await _noteAppService.DeleteNote(id);
        return NoContent();
    }
    [HttpGet]
    [Route("GetCaseSelectListItems")]
    public  async Task<ActionResult<IList<SelectItem>>> GetNoteCases()
    {
        var caseUsers =await  _noteAppService.GetNoteCases();

        if (caseUsers != null)
            return Ok(caseUsers);
        else
            return NotFound();
    }

}

