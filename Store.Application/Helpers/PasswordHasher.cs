using System.Security.Cryptography;
using System.Text;

namespace Store.Application.Helpers;

internal class PasswordHasher
{
    public static string HashPassword(string password)
    {
        using var sha512 = SHA512.Create();

        var hashedBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(password));

        return Convert.ToBase64String(hashedBytes);
    }

    public static bool VerifyPassword(string inputPassword, string hashedPassword)
    {
        var inputHashedPassword = HashPassword(inputPassword);

        return inputHashedPassword == hashedPassword;
    }
}