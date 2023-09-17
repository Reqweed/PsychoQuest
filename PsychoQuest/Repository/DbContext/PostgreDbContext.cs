using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Repository.DbContext;

public class PostgreDbContext : Microsoft.EntityFrameworkCore.DbContext
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
        modelBuilder.Entity<TestResults>()
            .Property(e => e.TestName)
            .HasConversion<string>();
        modelBuilder.Entity<User>()
            .Property(e => e.Gender)
            .HasConversion<string>();
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<TestResults> TestResults { get; set; }
}