using System.Buffers;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TNTU.ToDoApp.Domain.DTOs;
using TNTU.ToDoApp.Domain.Services;

namespace TNTU.ToDoApp.API.Controllers;

[Route("api/todo-items")]
[ApiController]
public class ToDoItemsController(ToDoItemsService toDoItemsService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<GetItemDto>>> GetToDoItems()
    {
        var items = await toDoItemsService.GetUserToDoItemsAsync();

        return Ok(items);
    }

    //[HttpPost]
    //public async Task<ActionResult> CreateToDoItem([FromBody] AddItemDto newItem)
    //{
    //}

    //[HttpPut]

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await toDoItemsService.DeleteToDoItemAsync(id);
        return Ok();
    }
}
