using QdaoCaseManager.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QdaoCaseManager.Dtos;
public class CreateUpdateNoteDto
{
    public int Id { get; set; }
    public string Content { get; set; }
    public int CaseId { get; set; }
}

