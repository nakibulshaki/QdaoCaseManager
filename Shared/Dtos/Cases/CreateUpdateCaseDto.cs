using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QdaoCaseManager.Shared.Dtos.Cases;
public class CreateUpdateCaseDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public CaseStatus Status { get; set; }
    public string? AssignedToUserId { get; set; }
}

