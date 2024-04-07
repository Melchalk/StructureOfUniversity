using System.ComponentModel.DataAnnotations;

namespace StructureOfUniversity.DTOs.Faculty.Requests;

public class CreateFacultyRequest
{
    [StringLength(100)]
    public required string Name { get; set; }

    [StringLength(100)]
    public required string DeanName { get; set; }
}
