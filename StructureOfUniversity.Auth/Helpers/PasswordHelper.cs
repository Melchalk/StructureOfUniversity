using System.Security.Cryptography;
using System.Text;

namespace StructureOfUniversity.Auth.Helpers;

public static class PasswordHelper
{
    private const string INTERNAL_SALT = "StructureOfUniversity.SALT";

    public static string GetPasswordHash(string userPhone, string userPassword, string salt)
    {
        return Encoding.UTF8.GetString(
            SHA256.HashData(Encoding.UTF8.GetBytes($"{salt}{userPhone}{userPassword}{INTERNAL_SALT}")));
    }

    public static void VerifyPasswordHash(string phoneRequest, string passwordRequest, string salt, string userPassword)
    {
        string requestPasswordHash = GetPasswordHash(phoneRequest, passwordRequest, salt);

        if (!string.Equals(userPassword, requestPasswordHash))
        {
            throw new ArgumentException("Not correct password");
        }
    }
}