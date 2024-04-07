namespace StructureOfUniversity.DTOs.Auth;

public class LoginRequest
{
    public required string Phone { get; set; }
    public required string Password { get; set; }
}
