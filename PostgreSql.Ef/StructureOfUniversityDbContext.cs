using Microsoft.EntityFrameworkCore;
using StructureOfUniversity.DbModels;
using StructureOfUniversity.PostgreSql.Ef.Interfaces;
using System.Reflection;

namespace StructureOfUniversity.PostgreSql.Ef;

public class StructureOfUniversityDbContext : DbContext, IDataProvider
{
    public DbSet<DbStudent> Students { get; set; }
    public DbSet<DbTeacher> Teachers { get; set; }
    public DbSet<DbFaculty> Faculties { get; set; }

    public DbSet<DbLog> LogRecords { get; set; }

    public StructureOfUniversityDbContext(
        DbContextOptions<StructureOfUniversityDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("StructureOfUniversity.DbModels"));
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