using Linqd.App.Models;
namespace Linqd.App.Services;

public class UserSessionService
{
    public User? CurrentUser { get; private set; }

    public bool IsLoggedIn => CurrentUser != null;

    public bool IsGuest => CurrentUser == null;

    public void login(User user)
    {
        CurrentUser = user;
    }

    public void logout()
    {
        CurrentUser = null;
    }
}