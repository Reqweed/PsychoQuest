using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Repository.DbContext;

public class PostgreDbContext : IdentityDbContext<User, IdentityRole<long>, long>
{
    private readonly IConfiguration _configuration;

    public PostgreDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetSection("PostgreSql:ConnectionString").Value);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(u => u.Id);
        modelBuilder.Entity<TestResults>()
            .Property(e => e.TestName)
            .HasConversion<string>();
        modelBuilder.Entity<User>()
            .Property(e => e.Gender)
            .HasConversion<string>();
        base.OnModelCreating(modelBuilder);
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<TestResults> TestResults { get; set; }
}