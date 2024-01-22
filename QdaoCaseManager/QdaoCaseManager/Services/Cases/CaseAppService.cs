using Microsoft.EntityFrameworkCore;
using QdaoCaseManager.Data;
using QdaoCaseManager.Dtos;
using QdaoCaseManager.Shared.Dtos;
using QdaoCaseManager.Shared.Entites;
using System.Drawing.Printing;

namespace QdaoCaseManager.Services.Cases;
public class CaseAppService : ICaseAppService
{
    private readonly ApplicationDbContext _dbContext;

    public CaseAppService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task CreateCase(CreateUpdateCaseDto createCaseDto)
    {
        _dbContext.Cases.Add(new Case
        {
            Tittle = createCaseDto.Tittle,
            Description = createCaseDto.Description,
            Status = createCaseDto.Status,
            AssignedToUserId = createCaseDto.AssignedToUserId
        });
        await _dbContext.SaveChangesAsync();
    }
    public async Task<IEnumerable<CaseDto>> GetCases(FilterCaseDto filterCaseDto)
    {
        var query =  _dbContext.Cases.AsQueryable();

        if(!String.IsNullOrWhiteSpace(filterCaseDto.QueryString)) {
            query = query.Where(x => x.Tittle.Contains(filterCaseDto.QueryString) ||
                                     x.Description.Contains(filterCaseDto.QueryString));
        }

        if (filterCaseDto.AssignedToUserId is not null)
            query = query.Where(x => x.AssignedToUserId == filterCaseDto.AssignedToUserId);

        if (filterCaseDto.CreateFrom is not null)
            query.Where(x => x.CreateDate >= filterCaseDto.CreateFrom);

        if (filterCaseDto.CreateTo is not null)
            query.Where(x => x.CreateDate <= filterCaseDto.CreateTo);

        var result =  await query
                        .Skip((filterCaseDto.CurrentPage - 1) * filterCaseDto.PageSize)
                        .Take(filterCaseDto.PageSize)
                        .Select(x=> new CaseDto { 
                            Id = x.Id,
                            Tittle= x.Tittle,
                            Description = x.Description,
                            Status = x.Status,
                            AssignedToUserName = x.AssignedToUser.UserName,
                            NumberOfNotes = x.Notes.Count(),
                            CreateDate = x.CreateDate
                            }).ToListAsync();
        return result;
    }

    public async Task<CaseDto> GetCaseById(int id)
    {
        var caseDto = await _dbContext.Cases
                    .Select(x => new CaseDto
                    {
                        Id = x.Id,
                        Tittle = x.Tittle,
                        Description = x.Description,
                        Status = x.Status,
                        AssignedToUserName = x.AssignedToUser.UserName,
                        NumberOfNotes = x.Notes.Count(),
                        CreateDate = x.CreateDate
                    }).FirstOrDefaultAsync();

        if(caseDto is null)
          throw new NullReferenceException($"Case not found with ID:{id}");

        return caseDto;
    }
    public async Task UpdateCase(int id, CreateUpdateCaseDto updatedCaseDto)
    {
        var caseEntry = await _dbContext.Cases.FindAsync(id);
        if (caseEntry is null) throw new NullReferenceException($"Case not found with ID:{id}");

        caseEntry.Tittle= updatedCaseDto.Tittle;
        caseEntry.Description= updatedCaseDto.Description; 
        caseEntry.Status= updatedCaseDto.Status;
        caseEntry.AssignedToUserId = updatedCaseDto.AssignedToUserId;

        await _dbContext.SaveChangesAsync();
    }
    public async Task DeleteCase(int id)
    {
        var caseEntry = await _dbContext.Cases.FindAsync(id);
        if (caseEntry is null) throw new NullReferenceException($"Case not found with ID:{id}");

        _dbContext.Cases.Remove(caseEntry);
        await _dbContext.SaveChangesAsync();
    }
}

