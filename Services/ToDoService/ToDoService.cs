using System;
using TodoApi.Data.UnitOfWork;
using TodoApi.DTOs.Responses;
using TodoApi.DTOs.ToDoItemDTOs;
using TodoApi.Model;
using TodoApi.Services.Authentication;

namespace TodoApi.Services.ToDoService;

public class ToDoService : IToDoService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;
    public ToDoService(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }
    public async Task<ItemDTO> CreateToDoAsync(ItemCreateDTO toDoItem)
    {
        if (toDoItem == null)
            throw new ArgumentNullException(nameof(toDoItem), "ToDo item cannot be null");
        int? userId = _currentUserService.GetUserId();
        if (userId == null)
            throw new UnauthorizedAccessException("User is not authenticated");
        var Item = new ToDoItem
        {
            Title = toDoItem.Title,
            Description = toDoItem.Description,
            UserId = userId.Value
        };
        await _unitOfWork.ToDos.CreateToDoItemAsync(Item);
        await _unitOfWork.CompleteAsync();
        return new ItemDTO
        {
            Id = Item.Id,
            Title = Item.Title,
            Description = Item.Description
        };

    }

    public async Task DeleteToDoAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero");
        int? userId = _currentUserService.GetUserId();
        if (userId == null)
            throw new UnauthorizedAccessException("User is not authorized to delete this item");
        await _unitOfWork.ToDos.DeleteToDoItemAsync(id);
        await _unitOfWork.CompleteAsync();
    }

    public async Task<PagedResult<ToDoItem>> GetAllToDosAsync(int page, int limit)
    {
        int? userId = _currentUserService.GetUserId();
        if (userId == null)
            throw new UnauthorizedAccessException("User is not authenticated");

        // Get all items first
        var allItems = await _unitOfWork.ToDos.GetAllToDoItemsAsync();

        // Filter by user ID
        var userItems = allItems.Where(item => item.UserId == userId.Value).ToList();

        if (!userItems.Any())
            throw new KeyNotFoundException("No ToDo items found for the current user");

        // Apply pagination
        var pagedItems = userItems
            .Skip((page - 1) * limit)
            .Take(limit)
            .ToList();

        return new PagedResult<ToDoItem>
        {
            Items = pagedItems,
            PageNumber = page,
            Limit = limit,
            TotalCount = userItems.Count
        };
    }


    public async Task<ToDoItem> GetToDoByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero");
        var toDoItem = await _unitOfWork.ToDos.GetToDoItemByIdAsync(id);
        int? userId = _currentUserService.GetUserId();
        if (userId == null)
            throw new UnauthorizedAccessException("User is not authenticated");
        if (toDoItem == null || toDoItem.UserId != userId.Value)
            throw new UnauthorizedAccessException("User is not authorized to access this ToDo item");
        return toDoItem;
    }

    public async Task<ToDoItem> UpdateToDoAsync(int id, ItemUpdateDTO toDoItem)
    {
        if (toDoItem == null)
            throw new ArgumentNullException(nameof(toDoItem));
        var item = await GetToDoByIdAsync(id);
        if (item.UserId != null && item.UserId != _currentUserService.GetUserId())
            throw new UnauthorizedAccessException("User is not authorized to update this ToDo item");
        var updated = await _unitOfWork.ToDos.UpdateToDoItemAsync(id, toDoItem);
        await _unitOfWork.CompleteAsync();
        return updated;
    }

}
