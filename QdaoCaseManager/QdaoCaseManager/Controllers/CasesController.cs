using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QdaoCaseManager.Data;
using QdaoCaseManager.Services.Cases;
using QdaoCaseManager.Shared.Entites;
namespace QdaoCaseManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CasesController : ControllerBase
{
    private readonly ICaseAppService _caseAppService;

    public CasesController(ICaseAppService caseAppService)
    {
        _caseAppService = caseAppService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Case>>> GetCase()
    {
        var cases = await _caseAppService.GetCases();
        if (cases != null)
            return Ok(cases);
        else
            return NotFound();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Case>> GetCase(int id)
    {
        Case caseEntry = await _caseAppService.GetCaseById(id);

        if (caseEntry == null)
            return NotFound();

        return caseEntry;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCase(int id, Case caseEntry)
    {
        await _caseAppService.UpdateCase(id, caseEntry);
        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<Case>> PostCase(Case caseEntry)
    {
        await _caseAppService.CreateCase(caseEntry);
        return CreatedAtAction("GetCase", new { id = caseEntry.Id }, caseEntry);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCase(int id)
    {
        await _caseAppService.DeleteCase(id);
        return NoContent();
    }
}

