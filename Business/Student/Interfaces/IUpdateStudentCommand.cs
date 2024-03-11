using DTOs.Student.Requests;

namespace Business.Student.Interfaces;

public interface IUpdateStudentCommand
{
    Task ExecuteAsync(UpdateStudentRequest request);
}
