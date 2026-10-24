
using Linqd.App.Models;

namespace Linqd.App.Data;

public class RsvpRepository
{
    private readonly LinqdDatabase _database;

    public RsvpRepository(LinqdDatabase database)
    {
        _database = database;
    }

    public async Task<int> AddRsvpAsync(Rsvp rsvp)
    {
        return await _database.InsertRsvpAsync(rsvp);
    }

    public async Task<List<Rsvp>> GetRsvpsByUserIdAsync(int userId)
    {
        return await _database.GetRsvpsByUserIdAsync(userId);
    }
    public async Task<Rsvp?> GetRsvpByUserAndEventAsync(
    int userId, int eventId)
    {
        return await _database.GetRsvpByUserAndEventAsync(
            userId, eventId);
    }
}
