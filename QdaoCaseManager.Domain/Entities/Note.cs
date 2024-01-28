namespace QdaoCaseManager.Domain.Entities;
public class Note : BaseAuditableEntity
{
    public int Id { get; set; }
    public string Content { get; set; }
    public int CaseId { get; set; }
    public Case Case { get; set; }
}

