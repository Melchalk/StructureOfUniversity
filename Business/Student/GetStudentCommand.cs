using Microsoft.AspNetCore.Http;
using DTOs.Student.Response;
using Business.Student.Interfaces;
using Data.Interfaces;

namespace Business.Student;

public class GetStudentCommand : IGetStudentCommand
{
    private IStudentsRepository _repository;

    public GetStudentCommand(IStudentsRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetStudentResponse?> ExecuteAsync(Guid id)
    {
        var student = await _repository.GetAsync(id);

        return student is null
            ? null
            : new()
            {
                Name = student.Name,
                Course = student.Course,
                University = student.University,
            };
    }

    public List<GetStudentResponse> GetAllStudents()
    {
        var students = new List<GetStudentResponse>();

        foreach (var student in _repository.GetStudents())
        {
            students.Add(new GetStudentResponse()
            {
                Name = student.Name,
                Course = student.Course,
                University = student.University,
            });
        }

        return students;
    }
}
