using StructureOfUniversity.DTOs.Enums;

namespace StructureOfUniversity.DTOs.Teacher.Response;

public class GetTeacherResponse
{
    public required string Name { get; set; }
    public TeachingPositions Position { get; set; }
    public string? Phone { get; set; }
    public int FacultyNumber { get; set; }
}
