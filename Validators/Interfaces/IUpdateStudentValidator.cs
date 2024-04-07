using FluentValidation;
using StructureOfUniversity.DTOs.Student.Requests;

namespace StructureOfUniversity.Validators.Interfaces;

public interface IUpdateStudentValidator : IValidator<UpdateStudentRequest>
{
}
