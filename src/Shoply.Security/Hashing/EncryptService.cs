using BCrypt.Net;

namespace Shoply.Security.Hashing;

public static class EncryptService
{
    private static readonly string Key = "$2a$11$252h2vGrxOa1D/ZO.SCree";

    public static string Encrypt(this string value) => BCrypt.Net.BCrypt.HashPassword(value, Key);

    public static bool CompareHash(this string value, string hash)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(hash) || !hash.StartsWith("$2"))
                return false;

            return BCrypt.Net.BCrypt.Verify(value, hash);
        }
        catch (SaltParseException)
        {
            return false;
        }
    }
}