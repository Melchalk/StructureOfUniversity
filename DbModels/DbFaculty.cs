using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace StructureOfUniversity.DbModels;

public class DbFaculty
{
    public const string TableName = "Faculties";

    public int Number { get; set; }

    [MaxLength(100)]
    public required string Name { get; set; }

    [MaxLength(100)]
    public required string DeanName { get; set; }

    [MaxLength(50)]
    public required string CreatedByPhone { get; set; }

    public ICollection<DbStudent> Students { get; set; }
    public ICollection<DbTeacher> Teachers { get; set; }

    public DbFaculty()
    {
        Students = new HashSet<DbStudent>();
        Teachers = new HashSet<DbTeacher>();
    }
}

public class DbFacultyConfiguration : IEntityTypeConfiguration<DbFaculty>
{
    public void Configure(EntityTypeBuilder<DbFaculty> builder)
    {
        builder.HasKey(f => f.Number);
    }
}