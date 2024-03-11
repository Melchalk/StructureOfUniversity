using DTOs.Student.Requests;

namespace Business.Student.Interfaces;

public interface ICreateStudentCommand
{
    Task<Guid?> ExecuteAsync(CreateStudentRequest request);
}
