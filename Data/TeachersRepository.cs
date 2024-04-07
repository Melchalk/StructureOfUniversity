using Microsoft.EntityFrameworkCore;
using StructureOfUniversity.Data.Interfaces;
using StructureOfUniversity.DbModels;
using StructureOfUniversity.PostgreSql.Ef.Interfaces;

namespace StructureOfUniversity.Data;

public class TeachersRepository : ITeachersRepository
{
    private readonly IDataProvider _provider;

    public TeachersRepository(IDataProvider provider)
    {
        _provider = provider;
    }

    public async Task CreateAsync(DbTeacher entity)
    {
        await _provider.Teachers.AddAsync(entity);

        await _provider.SaveAsync();
    }

    public async Task DeleteAsync(DbTeacher entity)
    {
        _provider.Teachers.Remove(entity);

        await _provider.SaveAsync();
    }

    public async Task<DbTeacher?> GetAsync(Guid id)
    {
        return await _provider.Teachers
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public DbSet<DbTeacher> GetTeachers() => _provider.Teachers;

    public void Save() => _provider.Save();

    public async Task SaveAsync() => await _provider.SaveAsync();
}
