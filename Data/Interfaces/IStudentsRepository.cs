using DbModels;
using DTOs.Student.Response;
using Microsoft.EntityFrameworkCore;

namespace Data.Interfaces;

public interface IStudentsRepository
{
    Task CreateAsync(DbStudent entity);

    Task<DbStudent?> GetAsync(Guid id);

    DbSet<DbStudent> GetStudents();

    Task DeleteAsync(DbStudent entity);

    void Save();

    Task SaveAsync();
}