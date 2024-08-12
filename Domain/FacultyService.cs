using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StructureOfUniversity.Data.Interfaces;
using StructureOfUniversity.DbModels;
using StructureOfUniversity.Domain.Helpers;
using StructureOfUniversity.Domain.Interfaces;
using StructureOfUniversity.DTOs;
using StructureOfUniversity.DTOs.Faculty.Requests;
using StructureOfUniversity.DTOs.Faculty.Response;
using StructureOfUniversity.Logging;
using StructureOfUniversity.Models.Exceptions;

namespace StructureOfUniversity.Domain;
public class FacultyService : IFacultyService
{
    private readonly IHttpContextAccessor _httpContext;
    private readonly IFacultiesRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    private readonly UniversityInfo _university;

    public FacultyService(
        IHttpContextAccessor httpContext,
        IFacultiesRepository repository,
        IMapper mapper,
        ILogger<FacultyService> logger,
        IOptions<UniversityInfo> university)
    {
        _httpContext = httpContext;
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
        _university = university.Value;
    }

    public async Task<int?> CreateAsync(CreateFacultyRequest request)
    {
        var faculty = _mapper.Map<DbFaculty>(request);

        faculty.CreatedByPhone = _httpContext.GetUserPhone();

        await _repository.CreateAsync(faculty);

        _logger.LogInformation(LoggerConstants.SUCCESSFUL_ACCESS_FACULTIES);

        return faculty.Number;
    }

    public async Task<GetFacultyResponse?> GetAsync(int number)
    {
        var faculty = await _repository.GetAsync(number)
            ?? throw new BadRequestException($"Faculty with number = '{number}' not found");

        _logger.LogInformation(LoggerConstants.SUCCESSFUL_ACCESS_FACULTIES);

        return _mapper.Map<GetFacultyResponse>(faculty);
    }

    public async Task<List<GetFacultyResponse>> GetFacultiesAsync()
    {
        _logger.LogInformation(LoggerConstants.SUCCESSFUL_ACCESS_FACULTIES);

        return await _mapper.ProjectTo<GetFacultyResponse>(
            _repository.GetFaculties())
            .ToListAsync();
    }
    public async Task UpdateAsync(UpdateFacultyRequest request)
    {
        var faculty = await _repository.GetAsync(request.Number)
            ?? throw new BadRequestException($"Faculty with number = '{request.Number}' not found");

        _logger.LogInformation(LoggerConstants.SUCCESSFUL_ACCESS_FACULTIES);

        faculty!.Name = request.Name ?? faculty.Name;
        faculty.DeanName = request.DeanName ?? faculty.DeanName;

        await _repository.SaveAsync();
    }

    public async Task DeleteAsync(int number)
    {
        var student = await _repository.GetAsync(number)
            ?? throw new BadRequestException("Faculty with this number not found");

        _logger.LogInformation(LoggerConstants.SUCCESSFUL_ACCESS_FACULTIES);

        await _repository.DeleteAsync(student);
    }

    public UniversityInfo GetUniversityInfo()
    {
        return _university;
    }
}