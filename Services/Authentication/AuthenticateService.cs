using System;
using Microsoft.AspNetCore.Identity;
using TodoApi.Data.UnitOfWork;
using TodoApi.DTOs.AuthenticationDTOs;
using TodoApi.Model;

namespace TodoApi.Services.Authentication;

public class AuthenticateService : IAuthenticateService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    private readonly IPasswordService _passwordService;

    public AuthenticateService(IUnitOfWork unitOfWork, ITokenService tokenService, IPasswordService passwordService)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
        _passwordService = passwordService;
    }

public async Task<AuthResponseDTO> RegisterAsync(RegisterDTO dto)
{
    // Check if user with email already exists
    var existingUser = await _unitOfWork.Users.GetUserByEmailAsync(dto.Email);
    if (existingUser != null)
        throw new InvalidOperationException("Email already exists");

    var existingUserName = await _unitOfWork.Users.GetUserByNameAsync(dto.Name);
    if (existingUserName != null)
        throw new InvalidOperationException("Username already exists");
    // Create new user
        var user = new User
    {
        Name = dto.Name,
        Email = dto.Email
    };

    user.PasswordHash = _passwordService.HashPassword(dto.Password);
    // Add user to context
    await _unitOfWork.Users.CreateUserAsync(user);
    await _unitOfWork.CompleteAsync();

    // Generate JWT token for the new user
    var token = _tokenService.GenerateToken(user.Id, user.Name);

    // Return authentication response
    return new AuthResponseDTO
    {
        Token = token
    };
}

    public async Task<AuthResponseDTO> LoginAsync(LoginDTO dto)
    {
        var user = await _unitOfWork.Users.GetUserByEmailAsync(dto.Email);
        if (user == null)
            throw new UnauthorizedAccessException("Invalid credentials");

        var result = _passwordService.VerifyPassword(dto.Password, user.PasswordHash);

        var token = _tokenService.GenerateToken(user.Id, user.Name);

        return new AuthResponseDTO
        {
            Token = token
        };
    }
}
