using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

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

    public static Expression<Func<ToDoItem, bool>> IsOverdueExpr() => 
        item => !item.IsCompleted && item.DueDate.HasValue && item.DueDate.Value < DateTime.Now;

    public bool IsOverdue() => 
        !IsCompleted && DueDate.HasValue && DueDate.Value < DateTime.Now && User.Name.Equals("John");
}
