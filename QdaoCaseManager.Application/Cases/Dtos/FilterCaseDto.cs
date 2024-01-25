using QdaoCaseManager.Application.Common.Models;
using QdaoCaseManager.Domain.Enums;

namespace QdaoCaseManager.Application.Cases.Dtos;
public class FilterCaseDto: PaginationBase
{
    public string?  SearchString { get; set; }
    public DateTime? CreateFrom { get; set; }
    public DateTime? CreateTo { get; set; }
    public string? AssignedToUserId { get; set; }
    public CaseStatus? Status { get; set; }

}
