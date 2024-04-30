using Microsoft.Extensions.Logging;
using StructureOfUniversity.PostgreSql.Ef.Interfaces;

namespace StructureOfUniversity.Logging;

public class DatabaseLoggerProvider : ILoggerProvider
{
    private readonly IDataProvider _provider;

    public DatabaseLoggerProvider(IDataProvider provider)
    {
        _provider = provider;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new DatabaseLogger(_provider);
    }

    public void Dispose() { }
}