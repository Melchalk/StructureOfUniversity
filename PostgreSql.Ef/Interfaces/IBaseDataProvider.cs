namespace PostgreSql.Ef.Interfaces;

public interface IBaseDataProvider
{
    Task SaveAsync();

    void Save();
}
