namespace TNTU.ToDoApp.Domain.Exceptions;

public class ItemNotFoundException : Exception
{
    public ItemNotFoundException(int itemId)
        : base($"Item with ID {itemId} not found.")
    {
    }
}
