using Microsoft.EntityFrameworkCore;
using StructureOfUniversity.Data.Interfaces;
using StructureOfUniversity.DbModels;
using StructureOfUniversity.PostgreSql.Ef.Interfaces;

namespace StructureOfUniversity.Data;

public class FacultiesRepository : IFacultiesRepository
{
    private readonly IDataProvider _provider;

    public FacultiesRepository(IDataProvider provider)
    {
        _provider = provider;
    }

    public async Task CreateAsync(DbFaculty entity)
    {
        await _provider.Faculties.AddAsync(entity);

        await _provider.SaveAsync();
    }

    public async Task DeleteAsync(DbFaculty entity)
    {
        _provider.Faculties.Remove(entity);

        await _provider.SaveAsync();
    }

    public async Task<DbFaculty?> GetAsync(int number)
    {
        return await _provider.Faculties
            .FirstOrDefaultAsync(s => s.Number == number);
    }

    public DbSet<DbFaculty> GetFaculties() => _provider.Faculties;

    public void Save() => _provider.Save();

    public async Task SaveAsync() => await _provider.SaveAsync();
}
