using Linqd.App.Models;

namespace Linqd.App.Data;

public class EventRepository
{
    private readonly LinqdDatabase _database;

    public EventRepository(LinqdDatabase database)
    {
        _database = database;
    }
    public async Task<int> AddEventAsync(Event newEvent)
    {
        return await _database.InsertEventAsync(newEvent);
    }
    public async Task<List<Event>> GetEventsAsync()
    {
        return await _database.GetEventsAsync();
    }
}