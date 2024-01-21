using Microsoft.EntityFrameworkCore;
using QdaoCaseManager.Data;
using QdaoCaseManager.Shared.Entites;

namespace QdaoCaseManager.Services.Cases;
public class CaseAppService : ICaseAppService
{
    private readonly ApplicationDbContext _dbContext;

    public CaseAppService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Case> CreateCase(Case caseEntry)
    {
        _dbContext.Cases.Add(caseEntry);
        await _dbContext.SaveChangesAsync();
        return caseEntry;
    }
    public async Task<IEnumerable<Case>> GetCases()
    {
        return await _dbContext.Cases.ToListAsync();
    }

    public async Task<Case> GetCaseById(int id)
    {
        var Case = await _dbContext.Cases.FindAsync(id);
        return Case;
    }
    public async Task<Case> UpdateCase(int id, Case updatedCase)
    {
        var caseEntry = await GetCaseById(id);
        if (caseEntry == null)
        {
            throw new InvalidOperationException("Case not found");
        }

        _dbContext.Entry(caseEntry).CurrentValues.SetValues(updatedCase);
        await _dbContext.SaveChangesAsync();
        return caseEntry;
    }
    public async Task DeleteCase(int id)
    {
        var caseEntry = await GetCaseById(id);
        if (caseEntry == null)
        {
            throw new InvalidOperationException("Case not found");
        }

        _dbContext.Cases.Remove(caseEntry);
        await _dbContext.SaveChangesAsync();
    }
}

