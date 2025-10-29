namespace TNTU.ToDoApp.Domain.DTOs;

public class GetItemDto
{
    public int Id { get; set; }

    public string Description { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime? DueDate { get; set; }
}
