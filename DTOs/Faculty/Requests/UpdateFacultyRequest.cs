using System.ComponentModel.DataAnnotations;

namespace StructureOfUniversity.DTOs.Faculty.Requests;

public class UpdateFacultyRequest
{
    public int Number { get; set; }

    [StringLength(100)]
    public string? Name { get; set; }

    [StringLength(100)]
    public string? DeanName { get; set; }
}
