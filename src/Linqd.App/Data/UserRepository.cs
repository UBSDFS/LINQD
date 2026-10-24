using Linqd.App.Models;

namespace Linqd.App.Data;

public class UserRepository
{
    private readonly LinqdDatabase _database;

    public UserRepository(LinqdDatabase database)
    {
        _database = database;
    }

    // Save a new user to SQLite.
    public async Task<int> AddUserAsync(User user)
    {
        return await _database.InsertUserAsync(user);
    }

    // Retrieve a user by username.
    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await _database.GetUserByUsernameAsync(username);
    }
}