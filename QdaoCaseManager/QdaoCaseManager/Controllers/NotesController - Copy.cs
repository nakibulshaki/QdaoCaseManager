using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QdaoCaseManager.Data;
using QdaoCaseManager.Dtos;
using QdaoCaseManager.Extra;
using QdaoCaseManager.Services.Notes;
using QdaoCaseManager.Shared.Dtos;
using QdaoCaseManager.Shared.Entites;
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
    public async Task<ActionResult<IList<SelectListItem>>> GetCaseHistories()
    {
        var caseHistories = await _dbContext.CaseHistories.ToListAsync();

        if (caseHistories != null)
            return Ok(caseHistories);
        else
            return NotFound();
    }

}

