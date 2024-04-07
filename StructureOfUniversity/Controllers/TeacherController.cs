using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StructureOfUniversity.Domain.Interfaces;
using StructureOfUniversity.DTOs.Teacher.Requests;
using StructureOfUniversity.DTOs.Teacher.Response;

namespace StructureOfUniversity.Controllers;

[Authorize]
[ApiController]
[Route("api/teacher")]
public class TeacherController(
    [FromServices] ITeacherService service) : ControllerBase
{
    [HttpGet("get/{id:Guid}")]
    public async Task<GetTeacherResponse?> GetTeacher(Guid id)
    {
        return await service.GetAsync(id);
    }

    [HttpGet("get/all")]
    public async Task<List<GetTeacherResponse>> GetTeachers()
    {
        return await service.GetTeachersAsync();
    }

    [HttpPut("update")]
    public async Task UpdateTeacher(
        [FromBody] UpdateTeacherRequest request)
    {
        await service.UpdateAsync(request);
    }

    [HttpDelete("delete")]
    public async Task DeleteTeacher(
        [FromQuery] Guid id)
    {
        await service.DeleteAsync(id);
    }
}
