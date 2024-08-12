using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StructureOfUniversity.Auth.Helpers;
using StructureOfUniversity.Auth.Services.Interfaces;
using StructureOfUniversity.DbModels;
using StructureOfUniversity.DTOs.Auth;
using StructureOfUniversity.DTOs.Enums;
using StructureOfUniversity.DTOs.Token;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace StructureOfUniversity.Auth.Services;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;
    private readonly TokenSettings _tokenSettings;

    public AuthService(
        IUserService userService,
        IOptions<TokenSettings> tokenSettings)
    {
        _userService = userService;
        _tokenSettings = tokenSettings.Value;
    }

    public async Task<LoginResult> LoginUser(LoginRequest request)
    {
        var user = await _userService.GetUserByPhone(request.Phone);

        PasswordHelper.VerifyPasswordHash(
            request.Phone,
            request.Password,
            user.Salt,
            user.Password);

        return new LoginResult
        {
            AccessToken = GenerateToken(user, TokenType.Access, out DateTime accessTokenLifetime),
            RefreshToken = GenerateToken(user, TokenType.Refresh, out DateTime refreshTokenLifetime),
        };
    }

    public string GenerateToken(DbTeacher user, TokenType tokenType, out DateTime tokenLifetime)
    {
        List<Claim> claims =
        [
            new Claim(ClaimTypes.Role, user.Position.ToString()),
            new Claim(ClaimTypes.MobilePhone, user.Phone),
            new Claim("TokenType", tokenType.ToString())
        ];

        tokenLifetime = DateTime.UtcNow.Add(
            tokenType == TokenType.Access
                ? TimeSpan.FromMinutes(_tokenSettings.AccessTokenLifetimeInMinutes)
                : TimeSpan.FromMinutes(_tokenSettings.RefreshTokenLifetimeInMinutes));

        var jwt = new JwtSecurityToken(
            issuer: _tokenSettings.TokenIssuer,
            audience: _tokenSettings.TokenAudience,
            claims: claims,
            expires: tokenLifetime,
            signingCredentials: new SigningCredentials(
                SigningSymmetricKey.GetKey(),
                SigningSymmetricKey.SigningAlgorithm));

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    public ClaimsPrincipal ValidateToken(string token)
    {
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = _tokenSettings.TokenIssuer,
            ValidateAudience = true,
            ValidAudience = _tokenSettings.TokenAudience,
            ValidateLifetime = true,
            IssuerSigningKey = SigningSymmetricKey.GetKey(),
            ValidateIssuerSigningKey = true
        };

        return new JwtSecurityTokenHandler()
            .ValidateToken(token, validationParameters, out _);
    }
}
