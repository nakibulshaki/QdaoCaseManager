using QdaoCaseManager.Shared.Entites;
namespace QdaoCaseManager.Services.Cases;
public interface ICaseAppService
{
    Task<Case> CreateCase(Case Case);
    Task<IEnumerable<Case>> GetCases();
    Task<Case> GetCaseById(int id);
    Task<Case> UpdateCase(int id, Case updatedCase);
    Task DeleteCase(int id);
}

