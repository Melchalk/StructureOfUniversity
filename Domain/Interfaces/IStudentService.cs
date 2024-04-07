using StructureOfUniversity.DTOs.Student.Requests;
using StructureOfUniversity.DTOs.Student.Response;

namespace StructureOfUniversity.Domain.Interfaces;

public interface IStudentService
{
    Task<Guid?> CreateAsync(CreateStudentRequest request);

    Task<GetStudentResponse?> GetAsync(Guid id);

    Task<List<GetStudentResponse>> GetStudentsAsync();

    Task UpdateAsync(UpdateStudentRequest request);

    Task DeleteAsync(Guid id);
}
