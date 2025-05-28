namespace TodoApi.DTOs.ToDoItemDTOs;

public record class ItemDTO
{
    public int Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
}
