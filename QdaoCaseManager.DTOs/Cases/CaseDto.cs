
using QdaoCaseManager.DTOs.Enums;

namespace QdaoCaseManager.DTOs.Cases;

public class CaseDto
{
    public int Id { get; set; }
    public string Tittle { get; set; }
    public string Description { get; set; }
    public CaseStatus Status { get; set; }
    public string StatusString => Status.ToString();
    public string? AssignedToUserName { get; set; }
    public int NumberOfNotes { get; set; }
    public DateTimeOffset Created { get; set; }
}


