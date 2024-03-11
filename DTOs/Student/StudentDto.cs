namespace DTOs.Student;

public class StudentDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public int Course { get; set; }
    public required string University { get; set; }
}
