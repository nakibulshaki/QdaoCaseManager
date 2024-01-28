using QdaoCaseManager.Application.Cases.Dtos;
using QdaoCaseManager.DTOs.Cases;
using QdaoCaseManager.DTOs.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QdaoCaseManager.Domain.Repositories;
public interface ICaseRepository
{
    Task CreateCase(CreateUpdateCaseDto Case);
    Task<PaginatedList<CaseDto>> GetCases(FilterCaseDto filterCaseDto);
    Task<CaseDto> GetCaseById(int id);
    Task UpdateCase(int id, CreateUpdateCaseDto updatedCase);
    Task DeleteCase(int id);
    Task<IList<SelectItem>> GetCaseUsers();
    Task<IList<SelectItem>> GetNotesSelectList();
    Task<CreateUpdateCaseDto> GetUpdateCaseById(int id);
}

