using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QdaoCaseManager.Entites;

namespace QdaoCaseManager.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Case> Cases { get; set; }
        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             modelBuilder.Entity<Case>()
            .Property(e => e.CreateDate)
            .HasDefaultValueSql("GETDATE()") // Use appropriate SQL function for your database
            .ValueGeneratedOnAdd(); // Only generate on entity creation;

             modelBuilder.Entity<Case>()
            .Property(e => e.UpdateDate)
            .HasDefaultValueSql("GETDATE()")
            .ValueGeneratedOnUpdate(); // Automatically update on add or update

            modelBuilder.Entity<Note>()
           .Property(e => e.CreateDate)
           .HasDefaultValueSql("GETDATE()") // Use appropriate SQL function for your database
           .ValueGeneratedOnAdd(); // Only generate on entity creation;

            modelBuilder.Entity<Note>()
           .Property(e => e.UpdateDate)
           .HasDefaultValueSql("GETDATE()")
           .ValueGeneratedOnUpdate(); // Automatically update on add or update

            base.OnModelCreating(modelBuilder);
        }
    }
}
