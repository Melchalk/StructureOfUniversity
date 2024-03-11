using DTOs.Student.Requests;
using FluentValidation;

namespace Validators.Interfaces;

public interface ICreateStudentValidator : IValidator<CreateStudentRequest>
{
}
