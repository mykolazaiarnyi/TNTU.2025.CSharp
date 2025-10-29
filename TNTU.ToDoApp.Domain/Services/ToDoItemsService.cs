using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using TNTU.ToDoApp.Data;
using TNTU.ToDoApp.Data.Models;
using TNTU.ToDoApp.Domain.DTOs;
using TNTU.ToDoApp.Domain.Exceptions;

namespace TNTU.ToDoApp.Domain.Services;

public class ToDoItemsService(ToDoContext context, ICurrentUserService currentUserService)
{
    public async Task<List<GetItemDto>> GetUserToDoItemsAsync()
    {
        var result = await context.ToDoItems
            .Where(t => t.GreatUserId == currentUserService.UserId)
            .Select(t => new GetItemDto
            {
                Id = t.Id,
                Description = t.Description,
                IsCompleted = t.IsCompleted,
                DueDate = t.DueDate
            })
            .ToListAsync();

        return result;
    }

    public async Task AddToDoItemAsync(AddItemDto item)
    {
        var user = await context.Users
            .FirstOrDefaultAsync(u => u.Id == currentUserService.UserId);

        var newItem = new ToDoItem
        {
            Description = item.Description,
            IsCompleted = false,
            DueDate = item.DueDate,
            //GreatUserId = currentUserService.UserId
            User = user!
        };
        context.ToDoItems.Add(newItem);
        await context.SaveChangesAsync();
    }

    public async Task UpdateToDoItemAsync(UpdateItemDto item)
    {
        var existingItem = await context.ToDoItems
            .FirstOrDefaultAsync(t => t.Id == item.Id && t.GreatUserId == currentUserService.UserId);

        if (existingItem == null)
        {
            throw new ItemNotFoundException(item.Id);
        }

        if (existingItem.IsCompleted 
            && existingItem.Description != item.Description)
        {
            throw new ItemIsCompletedException(item.Id);
        }

        existingItem.Description = item.Description;
        existingItem.IsCompleted = item.IsCompleted;
        existingItem.DueDate = item.DueDate;
        await context.SaveChangesAsync();
    }

    public async Task DeleteToDoItemAsync(int itemId)
    {
        var item = await context.ToDoItems
            .FirstOrDefaultAsync(t => t.Id == itemId && t.GreatUserId == currentUserService.UserId);

        if (item == null)
        {
            throw new ItemNotFoundException(itemId);
        }

        context.ToDoItems.Remove(item);
        await context.SaveChangesAsync();
    }
}
