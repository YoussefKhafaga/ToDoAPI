using System;
using TodoApi.DTOs.ToDoItemDTOs;
using TodoApi.Model;

namespace TodoApi.Repositories.ToDoRepository;

public interface IToDoRepository
{
    Task<ToDoItem> GetToDoItemByIdAsync(int id);
    Task<IEnumerable<ToDoItem>> GetAllToDoItemsAsync();
    Task<ToDoItem> CreateToDoItemAsync(ToDoItem item);
    Task<ToDoItem> UpdateToDoItemAsync(int id, ItemUpdateDTO item);
    Task<bool> DeleteToDoItemAsync(int id);
}
