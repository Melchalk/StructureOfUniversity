namespace StructureOfUniversity.DTOs.Student;

public class StudentDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public int Course { get; set; }
    public int FacultyNumber { get; set; }
}
