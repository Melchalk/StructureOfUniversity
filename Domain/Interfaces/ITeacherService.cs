using StructureOfUniversity.DTOs.Teacher.Requests;
using StructureOfUniversity.DTOs.Teacher.Response;

namespace StructureOfUniversity.Domain.Interfaces;

public interface ITeacherService
{
    Task<GetTeacherResponse?> GetAsync(Guid id);

    Task<List<GetTeacherResponse>> GetTeachersAsync();

    Task UpdateAsync(UpdateTeacherRequest request);

    Task DeleteAsync(Guid id);
}
