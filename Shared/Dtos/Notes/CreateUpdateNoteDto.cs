using QdaoCaseManager.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QdaoCaseManager.Dtos;
public class CreateUpdateNoteDto
{
    public int Id { get; set; }
    [Required]
    public string Content { get; set; }
    [Required]
    public int CaseId { get; set; }
}

