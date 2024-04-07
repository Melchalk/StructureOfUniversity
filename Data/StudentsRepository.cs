using Microsoft.EntityFrameworkCore;
using StructureOfUniversity.Data.Interfaces;
using StructureOfUniversity.DbModels;
using StructureOfUniversity.PostgreSql.Ef.Interfaces;

namespace StructureOfUniversity.Data;

public class StudentsRepository : IStudentsRepository
{
    private readonly IDataProvider _provider;

    public StudentsRepository(IDataProvider provider)
    {
        _provider = provider;
    }

    public async Task CreateAsync(DbStudent entity)
    {
        await _provider.Students.AddAsync(entity);

        await _provider.SaveAsync();
    }

    public async Task DeleteAsync(DbStudent entity)
    {
        _provider.Students.Remove(entity);

        await _provider.SaveAsync();
    }

    public async Task<DbStudent?> GetAsync(Guid id)
    {
        return await _provider.Students
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public DbSet<DbStudent> GetStudents() => _provider.Students;

    public void Save() => _provider.Save();

    public async Task SaveAsync() => await _provider.SaveAsync();
}
