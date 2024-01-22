using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QdaoCaseManager.Shared.Dtos;
public class PaginationBase
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
}

