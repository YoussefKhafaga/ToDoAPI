using System.ComponentModel.DataAnnotations;

namespace TodoApi.DTOs.AuthenticationDTOs;

public record class LoginDTO
{
    [Required, EmailAddress]
    public required string Email { get; set; }
    [Required, MinLength(6)]
    public required string Password { get; set; }
}
