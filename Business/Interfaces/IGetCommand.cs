using DTOs.Response;

namespace Business.Interfaces;

public interface IGetCommand
{
    GetStudentResponse? Execute(Guid id);

    List<GetStudentResponse> GetAllStudents();
}