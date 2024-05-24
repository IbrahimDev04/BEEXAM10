using BEExam10.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BEExam10.DataAccessLayer
{
    public class LumiaContext : IdentityDbContext<AppUser>
    {
        public LumiaContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Job> Jobs { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {

            var entries = ChangeTracker.Entries().ToList();

            foreach (var entry in entries)
            {
                if(entry.Entity is BaseEntity)
                {
                    switch(entry.State)
                    {
                        case EntityState.Added:
                            ((BaseEntity)entry.Entity).CreatedTime = DateTime.Now;
                            ((BaseEntity)entry.Entity).IsDeleted = false;
                            break;
                        case EntityState.Modified:
                            ((BaseEntity)entry.Entity).UpdatedTime = DateTime.Now;
                            break;
                    }
                }   
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
