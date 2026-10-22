using Linqd.App.Data;
using Linqd.App.Models;

namespace Linqd.App.Services;

public class EventService
{
    private readonly EventRepository _eventRepository;

    public EventService(EventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<int> AddEventAsync(Event newEvent)
    {
        return await _eventRepository.AddEventAsync(newEvent);
    }

    public async Task<List<Event>> GetEventsAsync()
    {
        return await _eventRepository.GetEventsAsync();
    }
}