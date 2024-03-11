using DTOs.Requests;

namespace Business.Interfaces;

public interface ICreateCommand
{
    Task<Guid?> ExecuteAsync(CreateStudentRequest request);
}
