using AutoMapper;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StructureOfUniversity.Data.Interfaces;
using StructureOfUniversity.DbModels;
using StructureOfUniversity.Domain.Interfaces;
using StructureOfUniversity.DTOs.Student.Requests;
using StructureOfUniversity.DTOs.Student.Response;
using StructureOfUniversity.Logging;
using StructureOfUniversity.Models.Exceptions;
using StructureOfUniversity.Validators.Interfaces;

namespace StructureOfUniversity.Domain;

public class StudentService : IStudentService
{
    private readonly IStudentsRepository _repository;
    private readonly ICreateStudentValidator _createValidator;
    private readonly IUpdateStudentValidator _updateValidator;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public StudentService(
        IStudentsRepository repository,
        ICreateStudentValidator createValidator,
        IUpdateStudentValidator updateValidator,
        IMapper mapper,
        ILogger<StudentService> logger)
    {
        _repository = repository;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Guid?> CreateAsync(CreateStudentRequest request)
    {
        ValidationResult result = _createValidator.Validate(request);

        if (!result.IsValid)
        {
            _logger.LogWarning("Not valid request");

            throw new BadRequestException(string.Join('\n', result.Errors));
        }

        var student = _mapper.Map<DbStudent>(request);

        await _repository.CreateAsync(student);

        _logger.LogInformation(LoggerConstants.SUCCESSFUL_ACCESS_STUDENTS);

        return student.Id;
    }

    public async Task<GetStudentResponse?> GetAsync(Guid id)
    {
        var student = await _repository.GetAsync(id)
            ?? throw new BadRequestException($"Student with id = '{id}' not found");

        _logger.LogInformation(LoggerConstants.SUCCESSFUL_ACCESS_STUDENTS);

        return _mapper.Map<GetStudentResponse>(student);
    }

    public async Task<List<GetStudentResponse>> GetStudentsAsync()
    {
        _logger.LogInformation(LoggerConstants.SUCCESSFUL_ACCESS_STUDENTS);

        return await _mapper.ProjectTo<GetStudentResponse>(
            _repository.GetStudents())
            .ToListAsync();
    }
    public async Task UpdateAsync(UpdateStudentRequest request)
    {
        ValidationResult result = _updateValidator.Validate(request);

        if (!result.IsValid)
        {
            _logger.LogError("Not valid request");

            throw new BadRequestException(string.Join('\n', result.Errors));
        }

        var student = await _repository.GetAsync(request.Id);

        _logger.LogInformation(LoggerConstants.SUCCESSFUL_ACCESS_STUDENTS);

        student!.Name = request.Name ?? student.Name;
        student.Course = request.Course ?? student.Course;
        student.FacultyNumber = request.FacultyNumber ?? student.FacultyNumber;

        await _repository.SaveAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var student = await _repository.GetAsync(id)
            ?? throw new BadRequestException($"Student with id = '{id}' not found");

        _logger.LogInformation(LoggerConstants.SUCCESSFUL_ACCESS_STUDENTS);

        await _repository.DeleteAsync(student);
    }
}