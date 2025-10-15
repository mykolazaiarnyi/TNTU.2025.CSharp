using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
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

var itemsGouped = await context.ToDoItems
    .Include(t => t.User)
    .Select(x => new
    {
        UserId = x.GreatUserId,
        x.Description,
        IsOverdueOnDb = !x.IsCompleted && x.DueDate.HasValue && x.DueDate.Value < DateTime.Now,
        IsOverdue = x.IsOverdue()
    })
    .ToListAsync();


var user1 = await context.Users
    .Include(u => u.ToDoItems)
    .FirstOrDefaultAsync(x => x.Id == user1Id);

var descriptions = user1?.ToDoItems?.Select(t => t.Description).ToList();

var items = await context.ToDoItems
    .Where(t => !t.IsCompleted && t.DueDate.HasValue && t.DueDate.Value < DateTime.Now)
    .Select(t => new
    {
        t.Id,
        t.Description,
        t.DueDate,
        UserName = t.User.Name,
        AlreadyExist = descriptions.Any(ut => ut == t.Description)
    })
    .ToListAsync();

var todoItem1 = user1?.ToDoItems?.FirstOrDefault();
todoItem1!.IsCompleted = false;

await context.SaveChangesAsync();
;