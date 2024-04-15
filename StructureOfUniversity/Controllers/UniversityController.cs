using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StructureOfUniversity.Domain.Interfaces;
using StructureOfUniversity.DTOs;

namespace StructureOfUniversity.Controllers;

[Authorize]
[ApiController]
[Route("api/faculty")]
public class UniversityController(
    [FromServices] IFacultyService service) : ControllerBase
{
    [HttpGet("university")]
    public UniversityInfo GetInfo()
    {
        return service.GetUniversityInfo();
    }
}
