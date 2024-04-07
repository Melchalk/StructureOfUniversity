using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StructureOfUniversity.DTOs.Enums;
using System.ComponentModel.DataAnnotations;

namespace StructureOfUniversity.DbModels;

public class DbTeacher
{
    public const string TableName = "Teachers";

    public Guid Id { get; set; }
    public int? FacultyNumber { get; set; }

    [MaxLength(100)]
    public required string Name { get; set; }
    public TeachingPositions Position { get; set; }
    public required string Phone { get; set; }

    [MaxLength(100)]
    public required string Password { get; set; }

    [MaxLength(100)]
    public required string Salt { get; set; }

    public DbFaculty? Faculty { get; set; }
}

public class DbTeacherConfiguration : IEntityTypeConfiguration<DbTeacher>
{
    public void Configure(EntityTypeBuilder<DbTeacher> builder)
    {
        builder.HasKey(t => t.Id);

        builder
            .HasOne(t => t.Faculty)
            .WithMany(f => f.Teachers)
            .HasForeignKey(t => t.FacultyNumber)
            .HasPrincipalKey(f => f.Number);
    }
}