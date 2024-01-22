using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QdaoCaseManager.Shared.Dtos;
public class FilterNoteDto: PaginationBase
{
    public string?  SearchString { get; set; }
}
