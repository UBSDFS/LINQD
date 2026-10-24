
using Linqd.App.Data;
using Linqd.App.Models;

namespace Linqd.App.Services;

public class RsvpService
{
    private readonly RsvpRepository _rsvpRepository;

    public RsvpService(RsvpRepository rsvpRepository)
    {
        _rsvpRepository = rsvpRepository;
    }

    // Save an RSVP to SQLite.
    public async Task<int> AddRsvpAsync(Rsvp rsvp)
    {
        return await _rsvpRepository.AddRsvpAsync(rsvp);
    }

    // Retrieve RSVP records for a logged-in user.
    public async Task<List<Rsvp>> GetRsvpsByUserIdAsync(int userId)
    {
        return await _rsvpRepository.GetRsvpsByUserIdAsync(userId);
    }
    public async Task<Rsvp?> GetRsvpByUserAndEventAsync(
    int userId, int eventId)
    {
        return await _rsvpRepository.GetRsvpByUserAndEventAsync(
            userId, eventId);
    }
}
