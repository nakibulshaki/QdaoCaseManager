using QdaoCaseManager.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QdaoCaseManager.Dtos;
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

