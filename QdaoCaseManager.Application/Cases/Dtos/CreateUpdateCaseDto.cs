using QdaoCaseManager.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace QdaoCaseManager.Application.Cases.Dtos;
public class CreateUpdateCaseDto
{
    public int Id { get; set; }
    
    [Required]
    public string Tittle { get; set; }
   
    [Required]
    public string Description { get; set; }
    
    [Required]
    public CaseStatus Status { get; set; }
    
    [Required]
    public string? AssignedToUserId { get; set; }
}

