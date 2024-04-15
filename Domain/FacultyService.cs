using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StructureOfUniversity.Data.Interfaces;
using StructureOfUniversity.DbModels;
using StructureOfUniversity.Domain.Interfaces;
using StructureOfUniversity.DTOs;
using StructureOfUniversity.DTOs.Faculty.Requests;
using StructureOfUniversity.DTOs.Faculty.Response;

namespace StructureOfUniversity.Domain;
public class FacultyService : IFacultyService
{
    private readonly IFacultiesRepository _repository;
    private readonly IMapper _mapper;
    private readonly UniversityInfo _university;

    public FacultyService(
        IFacultiesRepository repository,
        IMapper mapper,
        IOptions<UniversityInfo> university)
    {
        _repository = repository;
        _mapper = mapper;
        _university = university.Value;
    }

    public async Task<int?> CreateAsync(CreateFacultyRequest request)
    {
        var faculty = _mapper.Map<DbFaculty>(request);

        await _repository.CreateAsync(faculty);

        return faculty.Number;
    }

    public async Task<GetFacultyResponse?> GetAsync(int number)
    {
        var faculty = await _repository.GetAsync(number);

        return faculty is null
            ? null
            : _mapper.Map<GetFacultyResponse>(faculty);
    }

    public async Task<List<GetFacultyResponse>> GetFacultiesAsync()
    {
        return await _mapper.ProjectTo<GetFacultyResponse>(
            _repository.GetFaculties())
            .ToListAsync();
    }
    public async Task UpdateAsync(UpdateFacultyRequest request)
    {
        var faculty = await _repository.GetAsync(request.Number);

        faculty!.Name = request.Name ?? faculty.Name;
        faculty.DeanName = request.DeanName ?? faculty.DeanName;

        await _repository.SaveAsync();
    }

    public async Task DeleteAsync(int number)
    {
        var student = await _repository.GetAsync(number)
            ?? throw new ArgumentException("Faculty with this number not found");

        await _repository.DeleteAsync(student);
    }

    public UniversityInfo GetUniversityInfo()
    {
        return _university;
    }
}