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
    // Retrieve events hosted by a user.
    public async Task<List<Event>> GetHostedEventsAsync(int userId)
    {
        return await _database.GetHostedEventsAsync(userId);
    }
    public async Task<Event?> GetEventByIdAsync(int eventId)
    {
        return await _database.GetEventByIdAsync(eventId);
    }
}