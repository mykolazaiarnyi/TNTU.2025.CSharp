namespace TNTU.ToDoApp.Data.Models;

public class User
{
    public int Id { get; set; }

    public string Name { get; set; }

    public ICollection<ToDoItem> ToDoItems { get; set; }
}
