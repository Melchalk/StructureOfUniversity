using Microsoft.EntityFrameworkCore;
using StructureOfUniversity.DbModels;

namespace StructureOfUniversity.Data.Interfaces;

public interface ITeachersRepository
{
    Task CreateAsync(DbTeacher entity);

    Task<DbTeacher?> GetAsync(Guid id);

    DbSet<DbTeacher> GetTeachers();

    Task DeleteAsync(DbTeacher entity);

    void Save();

    Task SaveAsync();
}