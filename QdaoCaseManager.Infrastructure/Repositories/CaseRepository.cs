using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QdaoCaseManager.Application.Cases.Dtos;
using QdaoCaseManager.Domain.Entities;
using QdaoCaseManager.Domain.Repositories;
using QdaoCaseManager.DTOs.Cases;
using QdaoCaseManager.DTOs.Common.Models;
using QdaoCaseManager.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QdaoCaseManager.Infrastructure.Repositories;
public class CaseRepository : ICaseRepository
{
    private readonly ApplicationDbContext _dbContext;
    public CaseRepository(ApplicationDbContext dbContext)
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
        var query = _dbContext.Cases.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filterCaseDto.SearchString))
        {
            query = query.Where(x => x.Tittle.Contains(filterCaseDto.SearchString) ||
                                     x.Description.Contains(filterCaseDto.SearchString));
        }

        if (filterCaseDto.AssignedToUserId is not null)
            query = query.Where(x => x.AssignedToUserId == filterCaseDto.AssignedToUserId);

        if (filterCaseDto.Status is not null)
            query = query.Where(x => x.Status == filterCaseDto.Status);

        if (filterCaseDto.CreateFrom is not null)
            query.Where(x => x.Created >= filterCaseDto.CreateFrom);

        if (filterCaseDto.CreateTo is not null)
            query.Where(x => x.Created <= filterCaseDto.CreateTo);

        var queryResponse = await query
                             .Skip((filterCaseDto.CurrentPage - 1) * filterCaseDto.PageSize)
                             .Take(filterCaseDto.PageSize)
                             .OrderByDescending(x => x.Created)
                             .Select(x => new CaseDto
                             {
                                 Id = x.Id,
                                 Tittle = x.Tittle,
                                 Description = x.Description,
                                 Status = x.Status,
                                 AssignedToUserName = x.AssignedToUser.UserName,
                                 NumberOfNotes = x.Notes.Count(),
                                 Created = x.Created
                             }).ToListAsync();

        var result = new PaginatedList<CaseDto>(
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
                        Created = x.Created
                    }).FirstOrDefaultAsync();

        if (caseDto is null)
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

        caseEntry.Tittle = updatedCaseDto.Tittle;
        caseEntry.Description = updatedCaseDto.Description;
        caseEntry.Status = updatedCaseDto.Status;
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
    public async Task<IList<SelectItem>> GetCaseUsers()
    {
        var caseUsers = await _dbContext.Users
                                        .Select(x => new SelectItem
                                        {
                                            Value = x.Id,
                                            Text = x.UserName
                                        })
                                        .ToListAsync();

        return caseUsers;
    }
    public async Task<IList<SelectItem>> GetNotesSelectList()
    {
        var caseUsers = await _dbContext.Cases
                                        .Select(x => new SelectItem
                                        {
                                            Value = x.Id.ToString(),
                                            Text = x.Tittle
                                        })
                                        .ToListAsync();

        return caseUsers;
    }
}
