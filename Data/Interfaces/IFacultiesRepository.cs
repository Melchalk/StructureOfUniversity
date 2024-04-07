using Microsoft.EntityFrameworkCore;
using StructureOfUniversity.DbModels;

namespace StructureOfUniversity.Data.Interfaces;

public interface IFacultiesRepository
{
    Task CreateAsync(DbFaculty entity);

    Task<DbFaculty?> GetAsync(int number);

    DbSet<DbFaculty> GetFaculties();

    Task DeleteAsync(DbFaculty entity);

    void Save();

    Task SaveAsync();
}