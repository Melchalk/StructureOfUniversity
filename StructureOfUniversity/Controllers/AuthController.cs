using Microsoft.AspNetCore.Mvc;
using StructureOfUniversity.Auth.Services.Interfaces;
using StructureOfUniversity.DbModels;
using StructureOfUniversity.DTOs.Auth;
using StructureOfUniversity.DTOs.Enums;
using StructureOfUniversity.DTOs.Teacher.Requests;
using StructureOfUniversity.DTOs.Token;

namespace StructureOfUniversity.Controllers;

[Route("api/auth")]
public class AuthController(
    [FromServices] IUserService userService,
    [FromServices] IAuthService authService)
    : ControllerBase
{
    [HttpPost("login")]
    public async Task<LoginResult> LoginUser(
        [FromBody] LoginRequest request)
    {
        return await authService.LoginUser(request);
    }

    [HttpPost("register")]
    public async Task<LoginResult> RegisterUser([FromBody] CreateTeacherRequest request)
    {
        DbTeacher user = await userService.RegisterUser(request);

        return new LoginResult
        {
            AccessToken = authService.GenerateToken(user, TokenType.Access, out DateTime accessTokenLifetime),
            RefreshToken = authService.GenerateToken(user, TokenType.Refresh, out DateTime refreshTokenLifetime)
        };
    }
}
