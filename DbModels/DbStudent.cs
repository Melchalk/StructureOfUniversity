using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DbModels;

public class DbStudent
{
    public const string TableName = "Students";

    public Guid Id { get; set; }
    public required string Name { get; set; }
    public int Course { get; set; }
    public required string University { get; set; }
}

public class DbStudentConfiguration : IEntityTypeConfiguration<DbStudent>
{
    public void Configure(EntityTypeBuilder<DbStudent> builder)
    {
        builder.HasKey(s => s.Id);
    }
}