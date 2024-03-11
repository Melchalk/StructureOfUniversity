using Business.Student.Interfaces;
using Data.Interfaces;
using DbModels;
using DTOs.Student.Requests;
using FluentValidation.Results;
using Validators.Interfaces;

namespace Business.Student;

public class CreateStudentCommand : ICreateStudentCommand
{
    private IStudentsRepository _repository;
    private ICreateStudentValidator _validator;

    public CreateStudentCommand(
        IStudentsRepository repository,
        ICreateStudentValidator validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<Guid?> ExecuteAsync(CreateStudentRequest request)
    {
        ValidationResult result = _validator.Validate(request);

        if (!result.IsValid)
        {
            return null;
        }

        DbStudent student = new()
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Course = request.Course,
            University = request.University,
        };

        await _repository.CreateAsync(student);

        return student.Id;
    }
}
