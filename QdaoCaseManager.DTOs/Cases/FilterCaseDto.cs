using QdaoCaseManager.DTOs.Common.Models;
using QdaoCaseManager.DTOs.Enums;

namespace QdaoCaseManager.Application.Cases.Dtos;
public class FilterCaseDto: PaginationBase
{
    public string?  SearchString { get; set; }
    public DateTimeOffset? CreateFrom { get; set; }
    public DateTimeOffset? CreateTo { get; set; }
    public string? AssignedToUserId { get; set; }
    public CaseStatus? Status { get; set; }

}
