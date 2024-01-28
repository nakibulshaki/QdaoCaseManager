using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QdaoCaseManager.Domain.Entities;
using QdaoCaseManager.Infrastructure.identity;

namespace QdaoCaseManager.Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Case> Cases { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<CaseHistory> CaseHistories { get; set; }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            base.OnModelCreating(modelBuilder);
        }
    }
}
