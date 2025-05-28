using System;
using Microsoft.AspNetCore.Identity;
namespace TodoApi.Services.Authentication;

public class PasswordService : IPasswordService
{
    private readonly PasswordHasher<object> _passwordHasher;
    public PasswordService(PasswordHasher<object> passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }
    public string HashPassword(string password)
    {
        return _passwordHasher.HashPassword(null, password);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, password);
        return result == PasswordVerificationResult.Success;
    }
}
