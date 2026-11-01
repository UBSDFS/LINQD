
using System.Globalization;
using System.Text.Json;
using Linqd.App.Models;

namespace Linqd.App.Services;

public class ExternalEventService
{
    private readonly HttpClient _httpClient;

    public ExternalEventService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Retrieve public events from Ticketmaster.
    public async Task<List<ExternalEvent>> GetEventsAsync(
        string apiKey,
        string city = "Norfolk")
    {
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            throw new ArgumentException(
                "A Ticketmaster API key is required.",
                nameof(apiKey));
        }

        string url =
            "https://app.ticketmaster.com/discovery/v2/events.json" +
            $"?apikey={Uri.EscapeDataString(apiKey)}" +
            $"&city={Uri.EscapeDataString(city)}" +
            "&stateCode=VA" +
            "&size=10";

        using var response = await _httpClient.GetAsync(url);

        response.EnsureSuccessStatusCode();

        using var stream =
            await response.Content.ReadAsStreamAsync();

        using var document =
            await JsonDocument.ParseAsync(stream);

        var events = new List<ExternalEvent>();

        // Return an empty list if no events are found.
        if (!document.RootElement.TryGetProperty(
                "_embedded", out var embedded) ||
            !embedded.TryGetProperty(
                "events", out var eventArray) ||
            eventArray.ValueKind != JsonValueKind.Array)
        {
            return events;
        }

        foreach (var eventItem in eventArray.EnumerateArray())
        {
            var externalEvent = new ExternalEvent
            {
                Id = GetString(eventItem, "id"),
                Title = GetString(eventItem, "name"),
                Description = GetString(eventItem, "info"),
                EventUrl = GetString(eventItem, "url")
            };

            // Extract event date and time.
            if (eventItem.TryGetProperty(
                    "dates", out var dates) &&
                dates.TryGetProperty(
                    "start", out var start))
            {
                string dateTime = GetString(
                    start, "dateTime");

                if (DateTimeOffset.TryParse(
                        dateTime,
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out var parsedDate))
                {
                    externalEvent.StartDateTime =
                        parsedDate.LocalDateTime;
                }
                else
                {
                    string localDate = GetString(
                        start, "localDate");

                    string localTime = GetString(
                        start, "localTime");

                    if (DateTime.TryParse(
                            $"{localDate} {localTime}",
                            CultureInfo.InvariantCulture,
                            DateTimeStyles.None,
                            out var fallbackDate))
                    {
                        externalEvent.StartDateTime =
                            fallbackDate;
                    }
                }
            }

            // Extract venue information.
            if (eventItem.TryGetProperty(
                    "_embedded", out var details) &&
                details.TryGetProperty(
                    "venues", out var venues) &&
                venues.ValueKind == JsonValueKind.Array &&
                venues.GetArrayLength() > 0)
            {
                externalEvent.Location =
                    GetString(venues[0], "name");
            }

            // Extract an event image.
            if (eventItem.TryGetProperty(
                    "images", out var images) &&
                images.ValueKind == JsonValueKind.Array &&
                images.GetArrayLength() > 0)
            {
                externalEvent.ImageUrl =
                    GetString(images[0], "url");
            }

            events.Add(externalEvent);
        }

        return events;
    }

    // Safely retrieve a string property from JSON.
    private static string GetString(
        JsonElement element,
        string propertyName)
    {
        if (element.ValueKind != JsonValueKind.Object)
        {
            return string.Empty;
        }

        if (element.TryGetProperty(
                propertyName, out var value) &&
            value.ValueKind == JsonValueKind.String)
        {
            return value.GetString() ?? string.Empty;
        }

        return string.Empty;
    }
}
