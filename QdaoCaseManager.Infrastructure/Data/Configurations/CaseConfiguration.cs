using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QdaoCaseManager.Domain.Entities;
using System.Reflection.Emit;

namespace QdaoCaseManager.Infrastructure.Data.Configurations;

public class CaseConfiguration : IEntityTypeConfiguration<Case>
{
    public void Configure(EntityTypeBuilder<Case> builder)
    {
        builder.ToTable(tb => tb.HasTrigger("CaseDeleteTrigger"));

        builder.ToTable(tb => tb.HasTrigger("CaseUpdateTrigger"));
    }
}
