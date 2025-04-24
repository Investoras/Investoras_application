using Investoras_Backend.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Investoras_Backend.Data;

public class ApplicationDbContext(DbContextOptions options, IConfiguration configuration) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=FinancialApp;Username=postgres;Password=123");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


    }
}