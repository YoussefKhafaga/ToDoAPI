using System;

namespace TodoApi.Services.Authentication;

public interface ITokenService
{
    public string GenerateToken(int userId, string Name);
}
