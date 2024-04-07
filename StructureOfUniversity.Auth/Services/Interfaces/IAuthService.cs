using StructureOfUniversity.DbModels;
using StructureOfUniversity.DTOs.Auth;
using StructureOfUniversity.DTOs.Enums;
using StructureOfUniversity.DTOs.Token;

namespace StructureOfUniversity.Auth.Services.Interfaces;

public interface IAuthService
{
    Task<LoginResult> LoginUser(LoginRequest request);

    string GenerateToken(DbTeacher user, TokenType tokenType, out DateTime tokenLifetime);
}
