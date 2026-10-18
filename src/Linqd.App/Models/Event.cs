

namespace Linqd.App.Models;

public class Event
{

    public int EventId { get; set; }
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    public DateTime StartDateTime { get; set; }
    public string Location { get; set; } = string.Empty;
    public int? HostUserId { get; set; }
    public string Visibility { get; set; } = "Public";
}