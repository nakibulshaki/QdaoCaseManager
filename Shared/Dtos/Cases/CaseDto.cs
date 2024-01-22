using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QdaoCaseManager.Shared.Dtos;

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


