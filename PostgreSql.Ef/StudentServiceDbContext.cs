using Microsoft.EntityFrameworkCore;
using StructureOfUniversity.DbModels;
using StructureOfUniversity.PostgreSql.Ef.Interfaces;
using System.Reflection;

namespace PostgreSql.Ef;

public class StudentServiceDbContext : DbContext, IDataProvider
{
    public DbSet<DbStudent> Students { get; set; }
    public DbSet<DbTeacher> Teachers { get; set; }
    public DbSet<DbFaculty> Faculties { get; set; }

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