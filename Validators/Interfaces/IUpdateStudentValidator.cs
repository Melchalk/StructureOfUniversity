using DTOs.Requests;
using FluentValidation;

namespace Validators.Interfaces;

public interface IUpdateStudentValidator : IValidator<UpdateStudentRequest>
{
}
