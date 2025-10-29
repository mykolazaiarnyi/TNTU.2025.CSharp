using TNTU.ToDoApp.Data;
using TNTU.ToDoApp.Domain.Services;

var context = new ToDoContext(/* DbContextOptions here */);
var service = new ToDoItemsService(
    context, 
    new MockCurrentUserService());


// CRUD Operations Example
var newItem = new TNTU.ToDoApp.Data.Models.ToDoItem
{
    Description = "Finish the project",
    IsCompleted = false,
    DueDate = DateTime.Now.AddDays(7)
};
await service.AddToDoItemAsync(newItem);

var items = await service.GetUserToDoItemsAsync();
foreach (var item in items)
{
    Console.WriteLine($"{item.Id}: {item.Description} - Completed: {item.IsCompleted}");
}

newItem.IsCompleted = true;
await service.UpdateToDoItemAsync(newItem);

await service.DeleteToDoItemAsync(newItem.Id);
Console.WriteLine("Operations completed.");

;
try
{
    // Simulate some operations that may throw exceptions
    throw new InvalidOperationException("Simulated exception for demonstration.");
}
catch (Exception ex)
{
    var ex1 = (InvalidOperationException)ex;

    InvalidOperationException? ex2 = ex as InvalidOperationException;
    if (ex2 is not null)
    {
        Console.WriteLine($"An error occurred: {ex2.Message}");
    }

    bool isEx3 = ex is InvalidOperationException ex3; 
    if (isEx3)
    {
        //Console.WriteLine($"An error occurred: {ex3.Message}");
    }

    if (ex is InvalidOperationException)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
    }
}