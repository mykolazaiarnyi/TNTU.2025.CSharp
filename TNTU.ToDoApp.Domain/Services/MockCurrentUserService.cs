namespace TNTU.ToDoApp.Domain.Services;

public class MockCurrentUserService : ICurrentUserService
{
    public int UserId => Constants.UserId;
}
