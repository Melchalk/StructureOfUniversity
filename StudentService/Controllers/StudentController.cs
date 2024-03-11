using Business.Interfaces;
using DTOs.Requests;
using DTOs.Response;
using Microsoft.AspNetCore.Mvc;

namespace Lab11.Controllers;

[ApiController]
[Route("api/student")]
public class StudentController : ControllerBase
{
    [HttpGet("get")]
    public GetStudentResponse? GetStudent(
        [FromServices] IGetCommand command,
        [FromQuery] Guid id)
    {
        return command.Execute(id);
    }

    [HttpGet("get/file")]
    public IResult GetFileStudents()
    {
        string path = Directory.GetCurrentDirectory() + "/Students.json";
        string contentType = ".json";
        string downloadName = "Students.json";

        return Results.File(path, contentType, downloadName);
    }

    [HttpPost("create")]
    public async Task<Guid?> CreateStudent(
        [FromServices] ICreateCommand command,
        [FromBody] CreateStudentRequest request)
    {
        return await command.ExecuteAsync(request);
    }

    [HttpPut("update")]
    public async Task UpdateStudent(
        [FromServices] IUpdateCommand command,
        [FromBody] UpdateStudentRequest request)
    {
        await command.ExecuteAsync(request);
    }

    [HttpDelete("delete")]
    public async Task DeleteStudent(
        [FromServices] IDeleteCommand command,
        [FromQuery] Guid id)
    {
        await command.ExecuteAsync(id);
    }
}
