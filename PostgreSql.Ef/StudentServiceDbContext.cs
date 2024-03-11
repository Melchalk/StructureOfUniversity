using DbModels;
using Microsoft.EntityFrameworkCore;
using PostgreSql.Ef.Interfaces;
using System.Reflection;

namespace PostgreSql.Ef;

public class StudentServiceDbContext : DbContext, IDataProvider
{
    public DbSet<DbStudent> Students { get; set; }

    public StudentServiceDbContext(DbContextOptions<StudentServiceDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("DbModels"));
    }

    public async Task SaveAsync()
    {
        await SaveChangesAsync();
    }

    public void Save()
    {
        SaveChanges();
    }
}