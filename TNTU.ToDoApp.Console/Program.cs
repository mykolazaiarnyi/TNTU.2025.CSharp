using Microsoft.EntityFrameworkCore;
using TNTU.ToDoApp.Data;
using TNTU.ToDoApp.Data.Models;


//var user = new User
//{
//    Name = "John Doe 2",
//    ToDoItems = new List<ToDoItem>
//    {
//        new ToDoItem
//        {
//            Description = "Buy many groceries",
//            IsCompleted = false,
//            DueDate = DateTime.Now.AddDays(1)
//        }
//    }
//};


var context = new ToDoContext();

int user1Id = 1;
var user1 = await context.Users
    .Include(u => u.ToDoItems)
    .FirstOrDefaultAsync(x => x.Id == user1Id);

var todoItem1 = user1?.ToDoItems?.FirstOrDefault();
todoItem1!.IsCompleted = false;

await context.SaveChangesAsync();
;