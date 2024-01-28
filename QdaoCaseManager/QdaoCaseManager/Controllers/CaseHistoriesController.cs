using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QdaoCaseManager.DTOs.Common.Models;
using QdaoCaseManager.Infrastructure.Data;
namespace QdaoCaseManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CaseHistoriesController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public CaseHistoriesController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    [HttpGet]
    public async Task<ActionResult<IList<SelectItem>>> GetCaseHistories()
    {
        var caseHistories = await _dbContext.CaseHistories.ToListAsync();

        if (caseHistories != null)
            return Ok(caseHistories);
        else
            return NotFound();
    }

}

