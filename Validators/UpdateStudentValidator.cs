using FluentValidation;
using StructureOfUniversity.Validators.Interfaces;
using StructureOfUniversity.Data.Interfaces;
using StructureOfUniversity.DTOs.Student.Requests;

namespace StructureOfUniversity.Validators;

public class UpdateStudentValidator : AbstractValidator<UpdateStudentRequest>, IUpdateStudentValidator
{
    public UpdateStudentValidator(IStudentsRepository repository)
    {
        RuleFor(request => request.Course)
          .Must(a => a is null || a > 0 && a < 7)
          .WithMessage("Course must be positive and less or equals 6");

        RuleFor(request => request.Id)
          .Must(id => repository.GetAsync(id).Result is not null)
          .WithMessage("Student with this id not found");
    }
}
