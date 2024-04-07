namespace StructureOfUniversity.DTOs.Faculty.Response;

public class GetFacultyResponse
{
    public int Number { get; set; }
    public required string Name { get; set; }
    public required string DeanName { get; set; }
}
