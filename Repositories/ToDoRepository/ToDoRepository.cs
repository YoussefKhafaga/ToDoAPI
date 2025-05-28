using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.DTOs.ToDoItemDTOs;
using TodoApi.Model;

namespace TodoApi.Repositories.ToDoRepository;

public class ToDoRepository : IToDoRepository
{
    private readonly ToDoContext _context;

    public ToDoRepository(ToDoContext context)
    {
        _context = context;
    }

    public async Task<ToDoItem> CreateToDoItemAsync(ToDoItem item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item), "ToDo item cannot be null");
        await _context.Items.AddAsync(item);
        return item;
    }

    public async Task<bool> DeleteToDoItemAsync(int id)
    {
        var item = await _context.Items.FindAsync(id);
        if (item == null)
            throw new KeyNotFoundException($"ToDo item with ID {id} not found");
        _context.Items.Remove(item);
        return true;
    }

    public async Task<IEnumerable<ToDoItem>> GetAllToDoItemsAsync()
    {
        var items = await _context.Items.AsNoTracking().ToListAsync();
        return items;
    }

    public async Task<ToDoItem?> GetToDoItemByIdAsync(int id)
    {
        return await _context.Items.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<ToDoItem> UpdateToDoItemAsync(int id, ItemUpdateDTO item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item), "ToDo item cannot be null");

        var existingItem = await _context.Items.FindAsync(id);
        if (existingItem == null)
            throw new KeyNotFoundException($"ToDo item with ID {id} not found");

        existingItem.Title = item.Title;
        existingItem.Description = item.Description;

        _context.Items.Update(existingItem);
        return existingItem;
    }

}
