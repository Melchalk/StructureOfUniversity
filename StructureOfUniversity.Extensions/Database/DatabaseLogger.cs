using Microsoft.Extensions.Logging;
using StructureOfUniversity.PostgreSql.Ef.Interfaces;

namespace StructureOfUniversity.Logging;

public class DatabaseLogger : ILogger, IDisposable
{
    private readonly IDataProvider _provider;
    static object _lock = new();

    public DatabaseLogger(IDataProvider provider)
    {
        _provider = provider;
    }

    public void Log<TState>(
        LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        lock (_lock)
        {
            _provider.LogRecords.Add(new()
            {
                Message = formatter(state, exception),
                Level = logLevel,
                Time = DateTime.UtcNow,
            });

            _provider.Save();
        }
    }

    public IDisposable? BeginScope<TState>(
        TState state) where TState : notnull => this;

    public bool IsEnabled(LogLevel logLevel) => true;

    public void Dispose() { }
}