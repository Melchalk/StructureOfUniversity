using DTOs.Student.Response;

namespace Business.Student.Interfaces;

public interface IGetStudentCommand
{
    Task<GetStudentResponse?> ExecuteAsync(Guid id);

    List<GetStudentResponse> GetAllStudents();
}