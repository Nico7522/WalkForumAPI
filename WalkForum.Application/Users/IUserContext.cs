namespace WalkForum.Application.Users
{
    public interface IUserContext
    {
        CurrentUser GetCurrentUser();
    }
}