using System.ComponentModel.DataAnnotations;

namespace StructureOfUniversity.DTOs.Student.Requests;

public class CreateStudentRequest
{
    [StringLength(100)]
    public required string Name { get; set; }
    public int Course { get; set; }
    public int FacultyNumber { get; set; }
}
