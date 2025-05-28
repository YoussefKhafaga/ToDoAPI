using System;
using TodoApi.DTOs.AuthenticationDTOs;

namespace TodoApi.Services.Authentication;

public interface IAuthenticateService
{
    Task<AuthResponseDTO> RegisterAsync(RegisterDTO dto);
    Task<AuthResponseDTO> LoginAsync(LoginDTO dto);
}
