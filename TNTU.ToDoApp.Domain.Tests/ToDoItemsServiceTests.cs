using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using TNTU.ToDoApp.Data;
using TNTU.ToDoApp.Data.Models;
using TNTU.ToDoApp.Domain.DTOs;
using TNTU.ToDoApp.Domain.Exceptions;
using TNTU.ToDoApp.Domain.Services;

namespace TNTU.ToDoApp.Domain.Tests.Services;

public class ToDoItemsServiceTests
{
    private ToDoContext GetDbContextWithData(List<ToDoItem> items, List<User> users)
    {
        var options = new DbContextOptionsBuilder<ToDoContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var context = new ToDoContext(options);
        context.Users.AddRange(users);
        context.ToDoItems.AddRange(items);
        context.SaveChanges();
        return context;
    }

    [Fact]
    public async Task UpdateToDoItemAsync_ThrowsItemNotFoundException_WhenItemDoesNotExist()
    {
        // Arrange
        var userId = 1;
        var mockUserService = new Mock<ICurrentUserService>();
        mockUserService.Setup(x => x.UserId).Returns(userId);
        var context = GetDbContextWithData(new List<ToDoItem>(), new List<User>());
        var service = new ToDoItemsService(context, mockUserService.Object);
        var updateDto = new UpdateItemDto { Id = 99, Description = "desc", IsCompleted = false, DueDate = DateTime.Now };

        // Act & Assert
        await Assert.ThrowsAsync<ItemNotFoundException>(() => service.UpdateToDoItemAsync(updateDto));
    }

    [Fact]
    public async Task UpdateToDoItemAsync_ThrowsItemIsCompletedException_WhenItemIsCompletedAndDescriptionChanged()
    {
        // Arrange
        var userId = 1;
        var item = new ToDoItem
        {
            Id = 1,
            Description = "old",
            IsCompleted = true,
            DueDate = DateTime.Now,
            GreatUserId = userId
        };
        var mockUserService = new Mock<ICurrentUserService>();
        mockUserService.Setup(x => x.UserId).Returns(userId);
        var context = GetDbContextWithData(new List<ToDoItem> { item }, new List<User>());
        var service = new ToDoItemsService(context, mockUserService.Object);
        var updateDto = new UpdateItemDto { Id = 1, Description = "new", IsCompleted = true, DueDate = DateTime.Now };

        // Act & Assert
        await Assert.ThrowsAsync<ItemIsCompletedException>(() => service.UpdateToDoItemAsync(updateDto));
    }

    [Fact]
    public async Task UpdateToDoItemAsync_UpdatesItem_WhenValid()
    {
        // Arrange
        var userId = 1;
        var item = new ToDoItem
        {
            Id = 1,
            Description = "old",
            IsCompleted = false,
            DueDate = DateTime.Now,
            GreatUserId = userId
        };
        var mockUserService = new Mock<ICurrentUserService>();
        mockUserService.Setup(x => x.UserId).Returns(userId);
        var context = GetDbContextWithData(new List<ToDoItem> { item }, new List<User>());
        var service = new ToDoItemsService(context, mockUserService.Object);
        var newDueDate = DateTime.Now.AddDays(1);
        var updateDto = new UpdateItemDto { Id = 1, Description = "updated", IsCompleted = true, DueDate = newDueDate };

        // Act
        await service.UpdateToDoItemAsync(updateDto);

        // Assert
        var updated = await context.ToDoItems.FirstAsync(t => t.Id == 1);
        Assert.Equal("updated", updated.Description);
        Assert.True(updated.IsCompleted);
        Assert.Equal(newDueDate, updated.DueDate);
    }
}