using Microsoft.EntityFrameworkCore;
using NationalGeographicWorker.Domain.Entities.Occurrences;

namespace NationalGeographicWorker.Infrastructure.Persistence
{
    internal class NationalGeographicDbContext : DbContext
    {
        public NationalGeographicDbContext(DbContextOptions<NationalGeographicDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("postgis");
            modelBuilder.Entity<Occurrence>()
                .Property(o => o.Geolocation)
                .HasColumnType("geography(Point)")
                .HasColumnName("geolocation");
        }
            
        public DbSet<Occurrence> Occurrences { get; set; }
    }
}
