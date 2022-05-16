﻿using Microsoft.EntityFrameworkCore;
using Model.Domain.Entities;

namespace Server.Repository;

public class DataContext : DbContext
{
    private readonly string _connectionString;

    public DataContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public DbSet<Swimmer> Swimmers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(_connectionString);
    }
}