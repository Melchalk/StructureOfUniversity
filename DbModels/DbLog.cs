using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace StructureOfUniversity.DbModels;

public class DbLog
{
    public const string TableName = "LogRecords";

    public int Number { get; set; }
    public LogLevel Level { get; set; }
    public required string Message { get; set; }
    public DateTime Time { get; set; }
}

public class DbLogConfiguration : IEntityTypeConfiguration<DbLog>
{
    public void Configure(EntityTypeBuilder<DbLog> builder)
    {
        builder.HasKey(l => l.Number);
    }
}