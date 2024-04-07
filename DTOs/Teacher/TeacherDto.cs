using StructureOfUniversity.DTOs.Enums;

namespace StructureOfUniversity.DTOs.Teacher;

public class TeacherDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public TeachingPositions Position { get; set; }
    public required string Phone { get; set; }
    public int? FacultyNumber { get; set; }
}
