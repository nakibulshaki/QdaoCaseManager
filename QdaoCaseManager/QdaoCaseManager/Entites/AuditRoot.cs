using System.ComponentModel.DataAnnotations;
namespace QdaoCaseManager.Shared.Entites;

public class AuditRoot
{
    [Required]
    [DataType(DataType.Date)]
    public DateTime CreateDate { get; set; }
    [Required]
    [DataType(DataType.Date)]
    public DateTime UpdateDate { get; set; }
}

