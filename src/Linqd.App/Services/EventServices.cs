
using Linqd.App.Models;

namespace Linqd.App.Services;

public class EventService
{
    private readonly List<Event> _events = new();

    public void AddEvent(Event newEvent)
    {
        newEvent.EventId = _events.Count + 1;
        _events.Add(newEvent);
    }

    public List<Event> GetEvents()
    {
        return _events.ToList();
    }
}
