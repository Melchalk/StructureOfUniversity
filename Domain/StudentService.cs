using AutoMapper;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using StructureOfUniversity.Data.Interfaces;
using StructureOfUniversity.DbModels;
using StructureOfUniversity.Domain.Interfaces;
using StructureOfUniversity.DTOs.Student.Requests;
using StructureOfUniversity.DTOs.Student.Response;
using StructureOfUniversity.Validators.Interfaces;

namespace StructureOfUniversity.Domain;

public class StudentService : IStudentService
{
    private readonly IStudentsRepository _repository;
    private readonly ICreateStudentValidator _createValidator;
    private readonly IUpdateStudentValidator _updateValidator;
    private readonly IMapper _mapper;

    public StudentService(
        IStudentsRepository repository,
        ICreateStudentValidator createValidator,
        IUpdateStudentValidator updateValidator,
        IMapper mapper)
    {
        _repository = repository;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
        _mapper = mapper;
    }

    public async Task<Guid?> CreateAsync(CreateStudentRequest request)
    {
        ValidationResult result = _createValidator.Validate(request);

        if (!result.IsValid)
        {
            return null;
        }

        var student = _mapper.Map<DbStudent>(request);

        await _repository.CreateAsync(student);

        return student.Id;
    }

    public async Task<GetStudentResponse?> GetAsync(Guid id)
    {
        var student = await _repository.GetAsync(id);

        return student is null
            ? null
            : _mapper.Map<GetStudentResponse>(student);
    }

    public async Task<List<GetStudentResponse>> GetStudentsAsync()
    {
        return await _mapper.ProjectTo<GetStudentResponse>(
            _repository.GetStudents())
            .ToListAsync();
    }
    public async Task UpdateAsync(UpdateStudentRequest request)
    {
        ValidationResult result = _updateValidator.Validate(request);

        if (!result.IsValid)
        {
            throw new ArgumentException("Student with this id not found");
        }

        var student = await _repository.GetAsync(request.Id);

        student!.Name = request.Name ?? student.Name;
        student.Course = request.Course ?? student.Course;
        student.FacultyNumber = request.FacultyNumber ?? student.FacultyNumber;

        await _repository.SaveAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var student = await _repository.GetAsync(id)
            ?? throw new ArgumentException("Student with this id not found");

        await _repository.DeleteAsync(student);
    }
}
