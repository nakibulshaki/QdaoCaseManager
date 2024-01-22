using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QdaoCaseManager.Shared.Dtos;
public class CreateUpdateCaseDto
{
    public int Id { get; set; }
    public string Tittle { get; set; }
    public string Description { get; set; }
    public CaseStatus Status { get; set; }
    public string? AssignedToUserId { get; set; }
}

