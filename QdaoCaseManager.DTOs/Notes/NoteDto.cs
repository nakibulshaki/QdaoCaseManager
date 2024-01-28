namespace QdaoCaseManager.DTOs.Notes;

public record NoteDto
{
    public int Id { get; set; }
    public required string Content { get; set; }
    public required string CaseTittle { get; set; }
    public DateTimeOffset Created { get; set; }
    public string? CreatedBy { get; set; }
}

