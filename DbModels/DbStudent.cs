using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace StructureOfUniversity.DbModels;

public class DbStudent
{
    public const string TableName = "Students";

    public Guid Id { get; set; }
    public int FacultyNumber { get; set; }

    [MaxLength(100)]
    public required string Name { get; set; }
    public int Course { get; set; }

    public DbFaculty? Faculty { get; set; }
}

public class DbStudentConfiguration : IEntityTypeConfiguration<DbStudent>
{
    public void Configure(EntityTypeBuilder<DbStudent> builder)
    {
        builder.HasKey(s => s.Id);

        builder
            .HasOne(s => s.Faculty)
            .WithMany(f => f.Students)
            .HasForeignKey(s => s.FacultyNumber)
            .HasPrincipalKey(f => f.Number);
    }
}