namespace DTOs.Response;

public class GetStudentResponse
{
    public required string Name { get; set; }
    public int Course { get; set; }
    public required string University { get; set; }
}
