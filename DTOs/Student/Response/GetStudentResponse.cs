namespace StructureOfUniversity.DTOs.Student.Response;

public class GetStudentResponse
{
    public required string Name { get; set; }
    public int Course { get; set; }
    public int FacultyNumber { get; set; }
}
