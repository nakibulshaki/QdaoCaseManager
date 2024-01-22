using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QdaoCaseManager.Shared.Dtos;
public class FilterCaseDto: PaginationBase
{
    public string  QueryString { get; set; }
    public DateTime? CreateFrom { get; set; }
    public DateTime? CreateTo { get; set; }
    public string? AssignedToUserId { get; set; }
}
