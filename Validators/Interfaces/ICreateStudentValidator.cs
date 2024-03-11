using DTOs.Requests;
using FluentValidation;

namespace Validators.Interfaces;

public interface ICreateStudentValidator : IValidator<CreateStudentRequest>
{
}
