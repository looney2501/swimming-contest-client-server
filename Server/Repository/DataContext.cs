using Microsoft.EntityFrameworkCore;
using Model.Domain.Entities;

namespace Server.Repository;

public class DataContext : DbContext
{
    private string _connectionString;
    public DbSet<Swimmer> Swimmers { get; set; }

    public DataContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(_connectionString);
    }
}