using QdaoCaseManager.DTOs.Common.Models;

namespace QdaoCaseManager.DTOs.Notes;
public class FilterNoteDto : PaginationBase
{
    public string? SearchString { get; set; }
}
