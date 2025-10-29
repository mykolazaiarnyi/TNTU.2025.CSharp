namespace TNTU.ToDoApp.Domain.Exceptions;

public class ItemIsCompletedException : Exception
{
    public ItemIsCompletedException(int itemId)
        : base($"Item with ID {itemId} is already completed.")
    {
    }
}
