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
}