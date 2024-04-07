using FluentValidation;
using StructureOfUniversity.DTOs.Student.Requests;
using StructureOfUniversity.Validators.Interfaces;

namespace StructureOfUniversity.Validators;

public class CreateStudentValidator : AbstractValidator<CreateStudentRequest>, ICreateStudentValidator
{
    public CreateStudentValidator()
    {
        RuleFor(request => request.Course)
          .Must(a => a > 0 && a < 7)
          .WithMessage("Course must be positive and less or equals 6");
    }
}
