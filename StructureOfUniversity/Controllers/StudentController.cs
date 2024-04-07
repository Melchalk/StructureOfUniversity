using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StructureOfUniversity.Domain.Interfaces;
using StructureOfUniversity.DTOs.Student.Requests;
using StructureOfUniversity.DTOs.Student.Response;

namespace StructureOfUniversity.Controllers;

[Authorize]
[ApiController]
[Route("api/student")]
public class StudentController(
    [FromServices] IStudentService service) : ControllerBase
{
    [HttpGet("get")]
    public async Task<GetStudentResponse?> GetStudent([FromQuery] Guid id)
    {
        return await service.GetAsync(id);
    }

    [HttpGet("get/all")]
    public async Task<List<GetStudentResponse>> GetStudents()
    {
        return await service.GetStudentsAsync();
    }

    [Authorize(Policy = "More important than the assistant")]
    [HttpPost("create")]
    public async Task<Guid?> CreateStudent(
        [FromBody] CreateStudentRequest request)
    {
        return await service.CreateAsync(request);
    }

    [Authorize(Policy = "More important than the assistant")]
    [HttpPut("update")]
    public async Task UpdateStudent(
        [FromBody] UpdateStudentRequest request)
    {
        await service.UpdateAsync(request);
    }

    [Authorize(Policy = "More important than the assistant")]
    [HttpDelete("delete")]
    public async Task DeleteStudent(
        [FromQuery] Guid id)
    {
        await service.DeleteAsync(id);
    }
}
