using System;
using TodoApi.DTOs.Responses;
using TodoApi.DTOs.ToDoItemDTOs;
using TodoApi.Model;

namespace TodoApi.Services.ToDoService;

public interface IToDoService
{
    Task<PagedResult<ToDoItem>> GetAllToDosAsync(int page, int limit);
    Task<ToDoItem> GetToDoByIdAsync(int id);
    Task<ItemDTO> CreateToDoAsync(ItemCreateDTO toDoItem);
    Task<ToDoItem> UpdateToDoAsync(int id, ItemUpdateDTO toDoItem);
    Task DeleteToDoAsync(int id);
}
