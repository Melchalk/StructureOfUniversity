using StructureOfUniversity.DTOs;
using StructureOfUniversity.DTOs.Faculty.Requests;
using StructureOfUniversity.DTOs.Faculty.Response;

namespace StructureOfUniversity.Domain.Interfaces;

public interface IFacultyService
{
    Task<int?> CreateAsync(CreateFacultyRequest request);

    Task<GetFacultyResponse?> GetAsync(int number);

    Task<List<GetFacultyResponse>> GetFacultiesAsync();

    Task UpdateAsync(UpdateFacultyRequest request);

    Task DeleteAsync(int number);

    UniversityInfo GetUniversityInfo();
}
