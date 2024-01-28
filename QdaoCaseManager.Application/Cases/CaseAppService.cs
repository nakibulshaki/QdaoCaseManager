using Microsoft.AspNetCore.Identity;
using QdaoCaseManager.Application.Cases.Dtos;
using QdaoCaseManager.Domain.Entities;
using QdaoCaseManager.Domain.Repositories;
using QdaoCaseManager.DTOs.Cases;
using QdaoCaseManager.DTOs.Common.Models;
using QdaoCaseManager.Infrastructure.Data;
using QdaoCaseManager.Infrastructure.identity;

namespace QdaoCaseManager.Application.Cases;
public class CaseAppService:ICaseAppService
{
    private readonly ICaseRepository _caseRepository;

    public CaseAppService(ICaseRepository caseRepository)
    {
        _caseRepository = caseRepository;
    }

    public async Task CreateCase(CreateUpdateCaseDto createCaseDto)
    {
        await _caseRepository.CreateCase(createCaseDto);
    }

    public async Task<PaginatedList<CaseDto>> GetCases(FilterCaseDto filterCaseDto)
    {
        return await _caseRepository.GetCases(filterCaseDto);
    }

    public async Task<CaseDto> GetCaseById(int id)
    {
        return await _caseRepository.GetCaseById(id);
    }

    public async Task<CreateUpdateCaseDto> GetUpdateCaseById(int id)
    {
        return await _caseRepository.GetUpdateCaseById(id);
    }

    public async Task UpdateCase(int id, CreateUpdateCaseDto updatedCaseDto)
    {
        await _caseRepository.UpdateCase(id, updatedCaseDto);
    }

    public async Task DeleteCase(int id)
    {
        await _caseRepository.DeleteCase(id);
    }

    public async Task<IList<SelectItem>> GetCaseUsers()
    {
        return await _caseRepository.GetCaseUsers();
    }
}

