using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TNTU.ToDoApp.Data.Models;

public class ToDoItem
{
    public int Id { get; set; }

    [MaxLength(200)]
    public string Description { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? DueDate { get; set; }

    public int GreatUserId { get; set; }

    public User User { get; set; }
}
