using Elfie.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QdaoCaseManager.Data;
using QdaoCaseManager.Dtos;
using QdaoCaseManager.Extra;
using QdaoCaseManager.Shared.Dtos;
using QdaoCaseManager.Shared.Dtos.Cases;
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
    public async Task<PaginatedList<CaseDto>> GetCases(FilterCaseDto filterCaseDto)
    {
        var query =  _dbContext.Cases.AsQueryable();

        if(!String.IsNullOrWhiteSpace(filterCaseDto.SearchString)) {
            query = query.Where(x => x.Tittle.Contains(filterCaseDto.SearchString) ||
                                     x.Description.Contains(filterCaseDto.SearchString));
        }

        if (filterCaseDto.AssignedToUserId is not null)
            query = query.Where(x => x.AssignedToUserId == filterCaseDto.AssignedToUserId);

        if (filterCaseDto.Status is not null)
            query = query.Where(x => x.Status == filterCaseDto.Status);

        if (filterCaseDto.CreateFrom is not null)
            query.Where(x => x.CreateDate >= filterCaseDto.CreateFrom);

        if (filterCaseDto.CreateTo is not null)
            query.Where(x => x.CreateDate <= filterCaseDto.CreateTo);

        var queryResponse = await query
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

        var result = new  PaginatedList<CaseDto>(
                            queryResponse,
                            filterCaseDto.CurrentPage,
                            filterCaseDto.PageSize,
                            await query.CountAsync());
        
        return result;
    }

    public async Task<CaseDto> GetCaseById(int id)
    {
        var caseDto = await _dbContext.Cases
                    .Where(x => x.Id == id)
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
    public async Task<CreateUpdateCaseDto> GetUpdateCaseById(int id)
    {
        var caseDto = await _dbContext.Cases
                    .Where(x => x.Id == id)
                    .Select(x => new CreateUpdateCaseDto
                    {
                        Id = x.Id,
                        Tittle = x.Tittle,
                        Description = x.Description,
                        Status = x.Status,
                        AssignedToUserId = x.AssignedToUserId,
                    }).FirstOrDefaultAsync();

        if (caseDto is null)
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
    public async Task<IList<SelectListItem>> GetCaseUsers()
    {
        var caseUsers = await _dbContext.Users
                                        .Select( x => new SelectListItem { 
                                            Value = x.Id, 
                                            Text  = x.UserName })
                                        .ToListAsync();

        return caseUsers;
    }
}

