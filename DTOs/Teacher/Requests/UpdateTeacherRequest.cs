using StructureOfUniversity.DTOs.Enums;
using System.ComponentModel.DataAnnotations;

namespace StructureOfUniversity.DTOs.Teacher.Requests;

public class UpdateTeacherRequest
{
    public Guid Id { get; set; }

    [StringLength(100)]
    public string? Name { get; set; }
    public TeachingPositions? Position { get; set; }

    [RegularExpression(@"^\s*(\+7|8)\s*\(?(\d{3})\)?\s?(\d{3})[-\s]?(\d{2})[-\s]?(\d{2})\s*$", ErrorMessage = "Uncorrected phone number")]
    public string? Phone { get; set; }
    public int? FacultyNumber { get; set; }
}
