using DTOs.Requests;
using FluentValidation;
using Repository.Interfaces;
using Validators.Interfaces;

namespace Validators;

public class UpdateStudentValidator : AbstractValidator<UpdateStudentRequest>, IUpdateStudentValidator
{
    public UpdateStudentValidator(IStudentsRepository repository)
    {
        RuleFor(request => request.Course)
          .Must(a => a is null || (a > 0 && a < 7))
          .WithMessage("Course must be positive and less or equals 6");

        RuleFor(request => request.Id)
          .Must(id => repository.Students.FirstOrDefault(s => s.Id == id) is not null)
          .WithMessage("Student with this id not found");
    }
}
