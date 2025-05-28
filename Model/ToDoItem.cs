using System;

namespace TodoApi.Model;

public class ToDoItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int UserId { get; set; }
    public User? User { get; set; }
}
