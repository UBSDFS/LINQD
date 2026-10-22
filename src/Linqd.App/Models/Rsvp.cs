using SQLite;

namespace Linqd.App.Models;

public class Rsvp
{
    [PrimaryKey, AutoIncrement]
    public int RsvpId { get; set; }
    public int EventId { get; set; }
    public int? UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Response { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}