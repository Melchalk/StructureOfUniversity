namespace StructureOfUniversity.PostgreSql.Ef.Interfaces;

public interface IBaseDataProvider
{
    Task SaveAsync();

    void Save();
}
