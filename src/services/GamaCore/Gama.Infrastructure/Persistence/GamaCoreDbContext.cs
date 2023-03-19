using Gama.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gama.Infrastructure.Persistence;

public class GamaCoreDbContext : DbContext
{
    public GamaCoreDbContext(DbContextOptions<GamaCoreDbContext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }
}