using Gama.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gama.Infrastructure.Persistence;

public class GamaCoreDbContext : DbContext
{
    public GamaCoreDbContext(DbContextOptions<GamaCoreDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserRoles>()
            .HasKey(ur => new { ur.UserId, ur.RoleId });

        modelBuilder.Entity<UserRoles>()
            .HasOne<User>()
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId);

        modelBuilder.Entity<UserRoles>()
            .HasOne<Role>()
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId);

        modelBuilder.Entity<User>()
            .HasOne(u => u.Address)
            .WithOne(a => a.User)
            .HasForeignKey<UserAddress>(a => a.UserId);

        modelBuilder.Entity<TrafficFineTrafficViolation>()
            .HasKey(tv => new { tv.TrafficFineId, tv.TrafficViolationId });

        modelBuilder.Entity<TrafficFineTrafficViolation>()
            .HasOne(tv => tv.TrafficFine)
            .WithMany(tf => tf.TrafficFineTrafficViolations)
            .HasForeignKey(tv => tv.TrafficFineId);

        modelBuilder.Entity<TrafficFineTrafficViolation>()
            .HasOne(tv => tv.TrafficViolation)
            .WithMany(tv => tv.TrafficFineTrafficViolations)
            .HasForeignKey(tv => tv.TrafficViolationId);
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Occurrence> Occurrences { get; set; }

    public DbSet<TrafficFine> TrafficFines { get; set; }

    public DbSet<TrafficViolation> TrafficViolations { get; set; }

    public DbSet<UserAddress> UserAddress { get; set; }

    public DbSet<Role> Roles { get; set; }

    public DbSet<UserRoles> UserRoles { get; set; }
}