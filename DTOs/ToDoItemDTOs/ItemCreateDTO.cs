using System.ComponentModel.DataAnnotations;

namespace TodoApi.DTOs.ToDoItemDTOs;

public record class ItemCreateDTO
{
    [Required, Length(2, 20)]
    public string Title { get; set; } = string.Empty;
    [Required, Length(5, 100)]
    public string Description { get; set; } = string.Empty;
    [Required]
    public int UserId { get; set; }
}
