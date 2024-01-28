using QdaoCaseManager.Application.Cases.Dtos;
using QdaoCaseManager.DTOs.Cases;
using QdaoCaseManager.DTOs.Common.Models;

namespace QdaoCaseManager.Application.Cases;
public interface ICaseAppService
{
    Task CreateCase(CreateUpdateCaseDto Case);
    Task<PaginatedList<CaseDto>> GetCases(FilterCaseDto filterCaseDto);
    Task<CaseDto> GetCaseById(int id);
    Task UpdateCase(int id, CreateUpdateCaseDto updatedCase);
    Task DeleteCase(int id);
    Task<IList<SelectItem>> GetCaseUsers();
    Task<CreateUpdateCaseDto> GetUpdateCaseById(int id);
}

