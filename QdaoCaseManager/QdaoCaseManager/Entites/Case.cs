namespace QdaoCaseManager.Domain.Entities;
public class Case : AuditRoot
{
    public int Id { get; set; }
    public string Tittle { get; set; }
    public string Description { get; set; }
    public CaseStatus Status { get; set; }
    public string? AssignedToUserId { get; set; }
    public ApplicationUser AssignedToUser { get; set; }

    public ICollection<Note> Notes { get; set; }
}
