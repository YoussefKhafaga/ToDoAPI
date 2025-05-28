using System;
using System.ComponentModel.DataAnnotations;

namespace TodoApi.Model;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public List<ToDoItem> ToDoItems { get; set; } = new();
}
