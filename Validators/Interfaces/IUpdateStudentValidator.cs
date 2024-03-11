using DTOs.Student.Requests;
using FluentValidation;

namespace Validators.Interfaces;

public interface IUpdateStudentValidator : IValidator<UpdateStudentRequest>
{
}
