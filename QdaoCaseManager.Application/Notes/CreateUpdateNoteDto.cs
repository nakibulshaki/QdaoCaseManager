using System.ComponentModel.DataAnnotations;

namespace QdaoCaseManager.Application.Notes;
public class CreateUpdateNoteDto
{
    public int Id { get; set; }
    [Required]
    public string Content { get; set; }
    [Required]
    public int CaseId { get; set; }
}

