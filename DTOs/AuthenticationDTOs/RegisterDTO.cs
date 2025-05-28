using System.ComponentModel.DataAnnotations;

namespace TodoApi.DTOs.AuthenticationDTOs;

public record class RegisterDTO
{
    [Required, Length(3, 20)]
    public required string Name { get; set; }
    [Required, EmailAddress]
    public required string Email { get; set; }
    [Required, Length(8, 50), RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
        ErrorMessage = "Password must be at least 8 characters long and include uppercase, lowercase, number, and special character.")]
    public required string Password { get; set; }
}