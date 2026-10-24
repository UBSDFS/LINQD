using SQLite;
using Linqd.App.Models;
namespace Linqd.App.Data;

public class LinqdDatabase
{
    private readonly SQLiteAsyncConnection _database;

    public LinqdDatabase()
    {
        string databasePath = Path.Combine(
            FileSystem.AppDataDirectory,
            "linqd.db3"
        );

        _database = new SQLiteAsyncConnection(databasePath);
    }

    private readonly SemaphoreSlim _initLock = new(1, 1);
    private bool _initialized;

    public async Task InitializeAsync()
    {
        if (_initialized)
            return;

        await _initLock.WaitAsync();

        try
        {
            if (_initialized)
                return;

            await _database.CreateTableAsync<User>();
            await _database.CreateTableAsync<Event>();
            await _database.CreateTableAsync<Rsvp>();

            _initialized = true;
        }
        finally
        {
            _initLock.Release();
        }
    }
    public async Task<int> InsertEventAsync(Event newEvent)
    {
        await InitializeAsync();

        return await _database.InsertAsync(newEvent);
    }
    public async Task<List<Event>> GetEventsAsync()
    {
        await InitializeAsync();

        return await _database.Table<Event>().ToListAsync();
    }
    // Retrieve events created by a specific user.
    public async Task<List<Event>> GetHostedEventsAsync(int userId)
    {
        await InitializeAsync();

        return await _database.Table<Event>()
            .Where(eventItem => eventItem.HostUserId == userId)
            .OrderBy(eventItem => eventItem.StartDateTime)
            .ToListAsync();
    }
    // Save a new user to the local SQLite database.
    public async Task<int> InsertUserAsync(User user)
    {
        await InitializeAsync();

        return await _database.InsertAsync(user);
    }

    // Find an existing user by username.
    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        await InitializeAsync();

        return await _database.Table<User>()
            .Where(user => user.Username == username)
            .FirstOrDefaultAsync();
    }
    // Retrieve one event using its primary key.
    public async Task<Event?> GetEventByIdAsync(int eventId)
    {
        await InitializeAsync();

        return await _database.Table<Event>()
            .Where(eventItem => eventItem.EventId == eventId)
            .FirstOrDefaultAsync();
    }
    // Save an RSVP to SQLite.
    public async Task<int> InsertRsvpAsync(Rsvp rsvp)
    {
        await InitializeAsync();

        return await _database.InsertAsync(rsvp);
    }

    // Retrieve RSVPs for a specific user.
    public async Task<List<Rsvp>> GetRsvpsByUserIdAsync(int userId)
    {
        await InitializeAsync();

        return await _database.Table<Rsvp>()
            .Where(rsvp => rsvp.UserId == userId)
            .ToListAsync();
    }
    // Find an existing RSVP for a user and event.
    public async Task<Rsvp?> GetRsvpByUserAndEventAsync(
        int userId, int eventId)
    {
        await InitializeAsync();

        return await _database.Table<Rsvp>()
            .Where(rsvp =>
                rsvp.UserId == userId &&
                rsvp.EventId == eventId)
            .FirstOrDefaultAsync();
    }
}