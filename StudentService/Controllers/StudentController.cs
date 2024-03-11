using Business.Student.Interfaces;
using DTOs.Student.Requests;
using DTOs.Student.Response;
using Microsoft.AspNetCore.Mvc;

namespace Lab11.Controllers;

[ApiController]
[Route("api/student")]
public class StudentController : ControllerBase
{
    [HttpGet("get")]
    public async Task<GetStudentResponse?> GetStudent(
        [FromServices] IGetStudentCommand command,
        [FromQuery] Guid id)
    {
        return await command.ExecuteAsync(id);
    }

    [HttpGet("get/all")]
    public List<GetStudentResponse> GetStudents(
        [FromServices] IGetStudentCommand command)
    {
        return command.GetAllStudents();
    }

    [HttpPost("create")]
    public async Task<Guid?> CreateStudent(
        [FromServices] ICreateStudentCommand command,
        [FromBody] CreateStudentRequest request)
    {
        return await command.ExecuteAsync(request);
    }

    [HttpPut("update")]
    public async Task UpdateStudent(
        [FromServices] IUpdateStudentCommand command,
        [FromBody] UpdateStudentRequest request)
    {
        await command.ExecuteAsync(request);
    }

    [HttpDelete("delete")]
    public async Task DeleteStudent(
        [FromServices] IDeleteStudentCommand command,
        [FromQuery] Guid id)
    {
        await command.ExecuteAsync(id);
    }
}
