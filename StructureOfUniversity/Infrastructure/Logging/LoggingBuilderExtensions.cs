using StructureOfUniversity.Logging;
using StructureOfUniversity.PostgreSql.Ef.Interfaces;

namespace StructureOfUniversity.Infrastructure.Logging;

public static class LoggingBuilderExtensions
{
    public static ILoggingBuilder AddFile(this ILoggingBuilder builder, string path)
    {
        builder.AddProvider(new FileLoggerProvider(path));
        return builder;
    }

    public static ILoggingBuilder AddDatabase(this ILoggingBuilder builder, IDataProvider provider)
    {
        builder.AddProvider(new DatabaseLoggerProvider(provider));
        return builder;
    }
}
