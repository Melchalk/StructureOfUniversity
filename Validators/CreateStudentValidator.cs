using DTOs.Requests;
using FluentValidation;
using Validators.Interfaces;

namespace Validators;

public class CreateStudentValidator : AbstractValidator<CreateStudentRequest>, ICreateStudentValidator
{
    public CreateStudentValidator()
    {
        RuleFor(request => request.Course)
          .Must(a => a > 0 && a < 7)
          .WithMessage("Course must be positive and less or equals 6");
    }
}
