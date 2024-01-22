using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QdaoCaseManager.Shared.Dtos;

public class NoteDto
{
    public int Id { get; set; }
    public string Content { get; set; }
    public string CaseTittle { get; set; }
    public DateTime CreateDate { get; set; }
}


