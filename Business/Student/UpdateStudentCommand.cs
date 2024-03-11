using Business.Student.Interfaces;
using Data.Interfaces;
using DTOs.Student.Requests;
using FluentValidation.Results;
using Validators.Interfaces;

namespace Business.Student;

public class UpdateStudentCommand : IUpdateStudentCommand
{
    private IStudentsRepository _repository;
    private IUpdateStudentValidator _validator;

    public UpdateStudentCommand(
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
            throw new ArgumentException("Student with this id not found");
        }

        var student = await _repository.GetAsync(request.Id);

        student!.Name = request.Name ?? student.Name;
        student.Course = request.Course ?? student.Course;
        student.University = request.University ?? student.University;

        _repository.Save();
    }
}