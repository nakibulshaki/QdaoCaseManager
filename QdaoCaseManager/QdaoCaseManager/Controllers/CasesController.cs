using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QdaoCaseManager.Application.Cases;
using QdaoCaseManager.Application.Cases.Dtos;
using QdaoCaseManager.DTOs.Cases;
using QdaoCaseManager.DTOs.Common.Models;
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
    public async Task<ActionResult<PaginatedList<CaseDto>>> GetCase([FromQuery]FilterCaseDto filter)
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
    [HttpGet]

    [Route("GetUserSelectListItems")]
    public async Task<ActionResult<IList<SelectListItem>>> GetCaseUsers()
    {
        var caseUsers = await _caseAppService.GetCaseUsers();

        if (caseUsers != null)
            return Ok(caseUsers);
        else
            return NotFound();
    }
    [HttpGet("GetUpdateCase/{id}")]
    public async Task<ActionResult<CreateUpdateCaseDto>> GetUpdateCaseById(int id)
    {
        CreateUpdateCaseDto caseEntry = await _caseAppService.GetUpdateCaseById(id);
        if (caseEntry == null)
            return NotFound();

        return caseEntry;
    }
}

