using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QdaoCaseManager.Domain.Entities;
using System.Reflection.Emit;

namespace QdaoCaseManager.Infrastructure.Data.Configurations;

public class NoteConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
         builder
        .Property(e => e.Created)
        .HasDefaultValueSql("GETDATE()") // Use appropriate SQL function for your database
        .ValueGeneratedOnAdd(); // Only generate on entity creation;

        builder
       .Property(e => e.LastModified)
       .HasDefaultValueSql("GETDATE()")
       .ValueGeneratedOnUpdate(); // Automatically update on add or update

    }
}
