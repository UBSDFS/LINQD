
using Linqd.App.Models;

namespace Linqd.App.Services;

public class AuthenticationService
{
    private const string TestUsername = "testuser";
    private const string TestPassword = "password1";

    // Temporary in-memory accounts for this sprint.
    private readonly List<(User User, string Password)> _accounts = new();

    public bool ValidateLogin(string username, string password)
    {
        // Preserve our original test account.
        if (username == TestUsername && password == TestPassword)
        {
            return true;
        }

        // Check newly registered accounts.
        return _accounts.Any(account =>
            account.User.Username == username &&
            account.Password == password);
    }

    public User? GetUser(string username)
    {
        // Return the original test account.
        if (username == TestUsername)
        {
            return new User
            {
                UserId = 1,
                FirstName = "Test",
                LastName = "User",
                Email = "testuser@example.com",
                Username = TestUsername
            };
        }

        // Look for a newly registered account.
        return _accounts
            .FirstOrDefault(account =>
                account.User.Username == username)
            .User;
    }

    public bool RegisterUser(User user, string password)
    {
        // Prevent duplicate usernames and email addresses.
        if (user.Username.Equals(TestUsername, StringComparison.OrdinalIgnoreCase) ||
            _accounts.Any(account =>
                account.User.Username.Equals(
                    user.Username,
                    StringComparison.OrdinalIgnoreCase) ||
                account.User.Email.Equals(
                    user.Email,
                    StringComparison.OrdinalIgnoreCase)))
        {
            return false;
        }

        user.UserId = _accounts.Count + 2;

        _accounts.Add((user, password));

        return true;
    }
}
