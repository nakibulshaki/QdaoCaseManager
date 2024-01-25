using QdaoCaseManager.Application.Common.Models;

namespace QdaoCaseManager.Application.Notes.Dtos;
public class FilterNoteDto : PaginationBase
{
    public string? SearchString { get; set; }
}
