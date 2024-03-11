using Business.Interfaces;
using DTOs.Requests;
using FluentValidation.Results;
using Repository.Interfaces;
using Validators.Interfaces;

namespace Business;

public class UpdateCommand : IUpdateCommand
{
    private IStudentsRepository _repository;
    private IUpdateStudentValidator _validator;

    public UpdateCommand(
        IStudentsRepository repository,
        IUpdateStudentValidator validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task ExecuteAsync(UpdateStudentRequest request)
    {
        ValidationResult result = _validator.Validate(request);

        if (!result.IsValid)
        {
            return;
        }

        var student = _repository.Students.First(s => s.Id == request.Id);

        student.Name = request.Name ?? student.Name;
        student.Course = request.Course ?? student.Course;
        student.University = request.University ?? student.University;

        await _repository.SaveAsync();
    }
}
