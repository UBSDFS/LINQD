
using Linqd.App.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Linqd.App.Views;

[QueryProperty(nameof(EventId), "eventId")]
public partial class EventDetailsPage : ContentPage
{
    private readonly EventService _eventService;
    private int _eventId;

    public string EventId
    {
        get => _eventId.ToString();
        set
        {
            if (int.TryParse(value, out int parsedId))
            {
                _eventId = parsedId;
            }
            else
            {
                _eventId = 0;
            }
        }
    }

    public EventDetailsPage()
    {
        InitializeComponent();

        _eventService = IPlatformApplication.Current!
            .Services.GetRequiredService<EventService>();

        // Keep RSVP disabled until a valid event loads.
        RsvpButton.IsEnabled = false;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        try
        {
            await LoadEventDetailsAsync();
        }
        catch (Exception ex)
        {
            RsvpButton.IsEnabled = false;

            System.Diagnostics.Debug.WriteLine(
                $"Event details loading failed: {ex}");

            await DisplayAlertAsync(
                "Error",
                "Unable to load event details.",
                "OK");
        }
    }

    private async Task LoadEventDetailsAsync()
    {
        // Disable RSVP while loading event information.
        RsvpButton.IsEnabled = false;

        if (_eventId <= 0)
        {
            await DisplayAlertAsync(
                "Invalid Event",
                "No valid event was selected.",
                "OK");
            return;
        }

        // Retrieve the selected event from SQLite.
        var selectedEvent =
            await _eventService.GetEventByIdAsync(_eventId);

        if (selectedEvent == null)
        {
            await DisplayAlertAsync(
                "Event Not Found",
                "This event is no longer available.",
                "OK");
            return;
        }

        // Populate the event details.
        EventTitleLabel.Text = selectedEvent.Title;

        EventDateLabel.Text =
            selectedEvent.StartDateTime.ToString(
                "MMMM dd, yyyy - h:mm tt");

        EventLocationLabel.Text = selectedEvent.Location;

        EventDescriptionLabel.Text =
            selectedEvent.Description;

        // Enable RSVP once the event loads successfully.
        RsvpButton.IsEnabled = true;
    }

    // Navigate to RSVP page with selected EventId.
    private async void OnRsvpClicked(
        object sender, EventArgs e)
    {
        if (_eventId <= 0 || !RsvpButton.IsEnabled)
            return;

        await Shell.Current.GoToAsync(
            $"rsvp?eventId={_eventId}");
    }

    // Return to the previous page.
    private async void OnBackClicked(
        object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
