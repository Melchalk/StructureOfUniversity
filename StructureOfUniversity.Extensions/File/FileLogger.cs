using Microsoft.Extensions.Logging;

namespace StructureOfUniversity.Logging;

public class FileLogger: ILogger, IDisposable
{
    string filePath;
    static object _lock = new();

    public FileLogger(string path)
    {
        filePath = path;
    }

    public void Log<TState>(
        LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        lock (_lock)
        {
            File.AppendAllText(filePath, formatter(state, exception) + Environment.NewLine);
        }
    }

    public IDisposable? BeginScope<TState>(
        TState state) where TState : notnull => this;

    public bool IsEnabled(LogLevel logLevel) => true;

    public void Dispose() { }
}
