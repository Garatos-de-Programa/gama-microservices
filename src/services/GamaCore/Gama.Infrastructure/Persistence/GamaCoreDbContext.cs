using Gama.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Gama.Infrastructure.Persistence;

public class GamaCoreDbContext : DbContext
{
    public GamaCoreDbContext(DbContextOptions<GamaCoreDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserRoles>()
            .HasKey(ur => new { ur.UserId, ur.RoleId });

        modelBuilder.Entity<UserRoles>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.Roles)
            .HasForeignKey(ur => ur.UserId);

        modelBuilder.Entity<UserRoles>()
            .HasOne(ur => ur.Role)
            .WithMany()
            .HasForeignKey(ur => ur.RoleId);

        modelBuilder.Entity<TrafficFine>()
           .ToTable("traffic_fines");

        modelBuilder.Entity<TrafficViolation>()
           .ToTable("traffic_violations");

        modelBuilder.Entity<Role>()
           .ToTable("roles");

        modelBuilder.Entity<OccurrenceStatus>()
           .ToTable("occurrence_status");

        modelBuilder.Entity<OccurrenceUrgencyLevel>()
           .ToTable("occurrence_urgency_levels");

        modelBuilder.Entity<OccurrenceType>()
           .ToTable("occurrence_types");

        modelBuilder.Entity<OccurrenceType>()
           .ToTable("occurrence_types");

        modelBuilder.Entity<Occurrence>()
           .ToTable("occurrences");

        modelBuilder.Entity<TrafficFineTrafficViolation>()
            .ToTable("traffic_fine_traffic_violations")
            .HasKey(tv => new { tv.TrafficFineId, tv.TrafficViolationId });

        // Find all entities with DateTime properties
        var entitiesWithDateTimeProperties = modelBuilder.Model.GetEntityTypes()
            .Where(e => e.ClrType.GetProperties().Any(p => p.PropertyType == typeof(DateTime?) || p.PropertyType == typeof(DateTime)));

        // Configure a Value Converter for each DateTime property
        foreach (var entity in entitiesWithDateTimeProperties)
        {
            foreach (var property in entity.ClrType.GetProperties().Where(p => p.PropertyType == typeof(DateTime?) || p.PropertyType == typeof(DateTime)))
            {
                modelBuilder.Entity(entity.ClrType)
                    .Property(property.Name)
                    .HasConversion(new DateTimeConverter());
            }
        }
    }



    public DbSet<User> Users { get; set; }

    public DbSet<Occurrence> Occurrences { get; set; }

    public DbSet<TrafficFine> TrafficFines { get; set; }

    public DbSet<TrafficViolation> TrafficViolations { get; set; }

    public DbSet<TrafficFineTrafficViolation> TrafficFineTrafficViolations { get; set; }

    public DbSet<Role> Roles { get; set; }

    public DbSet<OccurrenceType> OccurrenceTypes { get; set; }

    public DbSet<OccurrenceStatus> OccurrenceStatus { get; set; }

    public DbSet<OccurrenceUrgencyLevel> OccurrenceUrgencyLevels { get; set; }
}

public class DateTimeConverter : ValueConverter<DateTime, DateTime>
{
    public DateTimeConverter() : base(
        fromDb => fromDb.ToUniversalTime(),
        toDb => DateTime.SpecifyKind(toDb, DateTimeKind.Utc))
    {
    }
}