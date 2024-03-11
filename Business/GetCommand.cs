using Business.Interfaces;
using DTOs.Response;
using Repository.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Business;

public class GetCommand : IGetCommand
{
    private IStudentsRepository _repository;

    public GetCommand(IStudentsRepository repository)
    {
        _repository = repository;
    }

    public GetStudentResponse? Execute(Guid id)
    {
        var student = _repository.Students
            .FirstOrDefault(x => x.Id == id);

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

        foreach(var student in _repository.Students)
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
