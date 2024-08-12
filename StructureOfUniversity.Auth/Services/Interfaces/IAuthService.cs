using StructureOfUniversity.DbModels;
using StructureOfUniversity.DTOs.Auth;
using StructureOfUniversity.DTOs.Enums;
using StructureOfUniversity.DTOs.Token;
using System.Security.Claims;

namespace StructureOfUniversity.Auth.Services.Interfaces;

public interface IAuthService
{
    Task<LoginResult> LoginUser(LoginRequest request);

    ClaimsPrincipal ValidateToken(string token);

    string GenerateToken(DbTeacher user, TokenType tokenType, out DateTime tokenLifetime);
}
