using System.ComponentModel.DataAnnotations;

namespace StructureOfUniversity.DTOs.Student.Requests;

public class UpdateStudentRequest
{
    public Guid Id { get; set; }

    [StringLength(100)]
    public string? Name { get; set; }
    public int? Course { get; set; }
    public int? FacultyNumber { get; set; }
}
