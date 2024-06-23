using Application.Contracts;

namespace Infrastructure.Authentication;

public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        var salt = BCrypt.Net.BCrypt.GenerateSalt(workFactor: 12);
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

        return hashedPassword;
    }

    public bool VerifyPassword(string password, string hashedPassword)
       => BCrypt.Net.BCrypt.Verify(password, hashedPassword);
}