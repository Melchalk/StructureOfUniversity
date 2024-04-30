using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StructureOfUniversity.Data.Interfaces;
using StructureOfUniversity.Domain.Interfaces;
using StructureOfUniversity.DTOs.Teacher.Requests;
using StructureOfUniversity.DTOs.Teacher.Response;
using StructureOfUniversity.Logging;

namespace StructureOfUniversity.Domain;
public class TeacherService : ITeacherService
{
    private readonly ITeachersRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public TeacherService(
        ITeachersRepository repository,
        IMapper mapper,
        ILogger<TeacherService> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<GetTeacherResponse?> GetAsync(Guid id)
    {
        var teacher = await _repository.GetAsync(id);

        _logger.LogInformation(LoggerConstants.SUCCESSFUL_ACCESS_TEACHERS);

        return teacher is null
            ? null
            : _mapper.Map<GetTeacherResponse>(teacher);
    }

    public async Task<List<GetTeacherResponse>> GetTeachersAsync()
    {
        _logger.LogInformation(LoggerConstants.SUCCESSFUL_ACCESS_TEACHERS);

        return await _mapper.ProjectTo<GetTeacherResponse>(
            _repository.GetTeachers())
            .ToListAsync();
    }

    public async Task UpdateAsync(UpdateTeacherRequest request)
    {
        var teacher = await _repository.GetAsync(request.Id);

        _logger.LogInformation(LoggerConstants.SUCCESSFUL_ACCESS_TEACHERS);

        teacher!.Name = request.Name ?? teacher.Name;
        teacher.Position = request.Position ?? teacher.Position;
        teacher.Phone = request.Phone ?? teacher.Phone;
        teacher.FacultyNumber = request.FacultyNumber ?? teacher.FacultyNumber;

        await _repository.SaveAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var student = await _repository.GetAsync(id)
            ?? throw new ArgumentException("Teacher with this id not found");

        _logger.LogInformation(LoggerConstants.SUCCESSFUL_ACCESS_TEACHERS);

        await _repository.DeleteAsync(student);
    }
}
