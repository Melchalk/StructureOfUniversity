using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace StructureOfUniversity.Auth.Helpers;

public static class SigningSymmetricKey
{
    private const string SIGNING_SECURITY_KEY = "H7rxZXtKVjFPBsLMUUOxp74iDnPbdaFOjrZhSOFHHbgfobp7y0M2lswkqs5NiTAeZO";

    private static readonly SymmetricSecurityKey _secretKey;

    public static string SigningAlgorithm => SecurityAlgorithms.HmacSha512;

    static SigningSymmetricKey()
    {
        _secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SIGNING_SECURITY_KEY));
    }

    public static SecurityKey GetKey() => _secretKey;
}
