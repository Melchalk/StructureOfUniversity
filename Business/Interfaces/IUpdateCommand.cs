using DTOs.Requests;

namespace Business.Interfaces;

public interface IUpdateCommand
{
    Task ExecuteAsync(UpdateStudentRequest request);
}
