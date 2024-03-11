namespace DTOs.Student.Requests;

public class CreateStudentRequest
{
    public required string Name { get; set; }
    public int Course { get; set; }
    public required string University { get; set; }
}
