using QdaoCaseManager.Domain.Enums;

namespace QdaoCaseManager.Application.Cases.Dtos;

public class CaseDto
{
    public int Id { get; set; }
    public string Tittle { get; set; }
    public string Description { get; set; }
    public CaseStatus Status { get; set; }
    public string StatusString => Status.ToString();
    public string? AssignedToUserName { get; set; }
    public int NumberOfNotes { get; set; }
    public DateTime CreateDate { get; set; }
}


