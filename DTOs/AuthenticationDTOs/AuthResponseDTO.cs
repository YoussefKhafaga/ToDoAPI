using System.ComponentModel.DataAnnotations;

namespace TodoApi.DTOs.AuthenticationDTOs;

public record class AuthResponseDTO
{
    [Required]
    public required string Token { get; set; }
}
