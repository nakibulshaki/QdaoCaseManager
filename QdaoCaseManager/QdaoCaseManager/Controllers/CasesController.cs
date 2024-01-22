using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QdaoCaseManager.Data;
using QdaoCaseManager.Services.Cases;
using QdaoCaseManager.Shared.Dtos;
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
    public async Task<ActionResult<IEnumerable<CaseDto>>> GetCase(FilterCaseDto filter)
    {
        var cases = await _caseAppService.GetCases(filter);
        if (cases != null)
            return Ok(cases);
        else
            return NotFound();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CaseDto>> GetCase(int id)
    {
        CaseDto caseEntry = await _caseAppService.GetCaseById(id);

        if (caseEntry == null)
            return NotFound();

        return caseEntry;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCase(int id, CreateUpdateCaseDto createUpdateCaseDto)
    {
        await _caseAppService.UpdateCase(id, createUpdateCaseDto);
        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<CaseDto>> PostCase(CreateUpdateCaseDto createUpdateCaseDto)
    {
        await _caseAppService.CreateCase(createUpdateCaseDto);
        return CreatedAtAction("GetCase", new { id = createUpdateCaseDto.Id }, createUpdateCaseDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCase(int id)
    {
        await _caseAppService.DeleteCase(id);
        return NoContent();
    }
}

