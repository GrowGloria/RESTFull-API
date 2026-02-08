using Microsoft.EntityFrameworkCore;
using RESTFull_API.Models;

namespace RESTFull_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Roll> Rolls => Set<Roll>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Roll>(e =>
            {
                e.Property(x => x.Id).HasDefaultValueSql("gen_random_uuid()");

                e.Property(x => x.Length).HasPrecision(18, 3);
                e.Property(x => x.Weight).HasPrecision(18, 3);

                e.HasIndex(x => x.AddedAt);
                e.HasIndex(x => x.RemovedAt);
            });
        }
    }
}
