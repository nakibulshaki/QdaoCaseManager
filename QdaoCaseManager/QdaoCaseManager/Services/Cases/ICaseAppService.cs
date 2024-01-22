using QdaoCaseManager.Dtos;
using QdaoCaseManager.Shared.Dtos;
using QdaoCaseManager.Shared.Entites;
namespace QdaoCaseManager.Services.Cases;
public interface ICaseAppService
{
    Task CreateCase(CreateUpdateCaseDto Case);
    Task<IEnumerable<CaseDto>> GetCases(FilterCaseDto filterCaseDto);
    Task<CaseDto> GetCaseById(int id);
    Task UpdateCase(int id, CreateUpdateCaseDto updatedCase);
    Task DeleteCase(int id);
}

