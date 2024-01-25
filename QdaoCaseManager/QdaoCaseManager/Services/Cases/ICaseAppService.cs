﻿namespace QdaoCaseManager.Application.Cases;
public interface ICaseAppService
{
    Task CreateCase(CreateUpdateCaseDto Case);
    Task<PaginatedList<CaseDto>> GetCases(FilterCaseDto filterCaseDto);
    Task<CaseDto> GetCaseById(int id);
    Task UpdateCase(int id, CreateUpdateCaseDto updatedCase);
    Task DeleteCase(int id);
    Task<IList<SelectListItem>> GetCaseUsers();
    Task<CreateUpdateCaseDto> GetUpdateCaseById(int id);
}

