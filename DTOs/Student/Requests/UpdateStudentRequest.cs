namespace DTOs.Student.Requests;

public class UpdateStudentRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public int? Course { get; set; }
    public string? University { get; set; }
}
