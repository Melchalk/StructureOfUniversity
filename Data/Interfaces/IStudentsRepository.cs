using Microsoft.EntityFrameworkCore;
using StructureOfUniversity.DbModels;

namespace StructureOfUniversity.Data.Interfaces;

public interface IStudentsRepository
{
    Task CreateAsync(DbStudent entity);

    Task<DbStudent?> GetAsync(Guid id);

    DbSet<DbStudent> GetStudents();

    Task DeleteAsync(DbStudent entity);

    void Save();

    Task SaveAsync();
}