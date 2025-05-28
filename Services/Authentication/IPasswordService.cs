using System;

namespace TodoApi.Services.Authentication;

public interface IPasswordService
{
    bool VerifyPassword(string password, string hashedPassword);
    string HashPassword(string password);
}
