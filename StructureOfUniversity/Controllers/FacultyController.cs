using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StructureOfUniversity.Domain.Interfaces;
using StructureOfUniversity.DTOs.Faculty.Requests;
using StructureOfUniversity.DTOs.Faculty.Response;

namespace StructureOfUniversity.Controllers;

//[Authorize]
[ApiController]
[Route("api/faculty")]
public class FacultyController(
    [FromServices] IFacultyService service) : ControllerBase
{
    [HttpGet("get/{number:int}")]
    public async Task<GetFacultyResponse?> GetFaculty(int number)
    {
        return await service.GetAsync(number);
    }

    [HttpGet("get/all")]
    public async Task<List<GetFacultyResponse>> GetFaculties()
    {
        return await service.GetFacultiesAsync();
    }

    [Authorize(Policy = "More important than the assistant")]
    [HttpPost("create")]
    public async Task<int?> CreateFaculty(
        [FromBody] CreateFacultyRequest request)
    {
        return await service.CreateAsync(request);
    }

    [Authorize(Policy = "More important than the assistant")]
    [HttpPut("update")]
    public async Task UpdateFaculty(
        [FromBody] UpdateFacultyRequest request)
    {
        await service.UpdateAsync(request);
    }

    [Authorize(Policy = "More important than the assistant")]
    [HttpDelete("delete")]
    public async Task DeleteFaculty(
        [FromQuery] int number)
    {
        await service.DeleteAsync(number);
    }
}
