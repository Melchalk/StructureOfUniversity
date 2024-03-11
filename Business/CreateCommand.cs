using Business.Interfaces;
using DTOs;
using DTOs.Requests;
using FluentValidation.Results;
using Repository.Interfaces;
using Validators.Interfaces;

namespace Business;

public class CreateCommand : ICreateCommand
{
    private IStudentsRepository _repository;
    private ICreateStudentValidator _validator;

    public CreateCommand(
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

        StudentDto student = new()
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Course = request.Course,
            University = request.University,
        };

        _repository.Students.Add(student);

        await _repository.SaveAsync();

        return student.Id;
    }
}
