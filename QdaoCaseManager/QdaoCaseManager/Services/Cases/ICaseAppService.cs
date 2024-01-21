using QdaoCaseManager.Shared.Dtos.Cases;
using QdaoCaseManager.Shared.Entites;
namespace QdaoCaseManager.Services.Cases;
public interface ICaseAppService
{
    Task<Case> CreateCase(CreateUpdateCaseDto Case);
    Task<IEnumerable<CaseDto>> GetCases(FilterCaseDto filterCaseDto);
    Task<CaseDto> GetCaseById(int id);
    Task UpdateCase(int id, Case updatedCase);
    Task DeleteCase(int id);
}

