
namespace Linqd.App.Models;

public class ExternalEvent
{
    public string Id { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime? StartDateTime { get; set; }

    public string Location { get; set; } = string.Empty;

    public string ImageUrl { get; set; } = string.Empty;

    public string EventUrl { get; set; } = string.Empty;
}
