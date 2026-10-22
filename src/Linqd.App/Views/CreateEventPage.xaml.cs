
using Linqd.App.Models;
using Linqd.App.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Linqd.App.Views;

public partial class CreateEventPage : ContentPage
{
    // Shared services for user sessions and event storage.
    private readonly UserSessionService _sessionService;
    private readonly EventService _eventService;

    public CreateEventPage()
    {
        InitializeComponent();

        // Retrieve services registered in MauiProgram.cs.
        _sessionService = IPlatformApplication.Current!
            .Services.GetRequiredService<UserSessionService>();

        _eventService = IPlatformApplication.Current!
            .Services.GetRequiredService<EventService>();
    }

    private async void OnCreateEventClicked(object sender, EventArgs e)
    {
        // Only authenticated users can create events.
        if (!_sessionService.IsLoggedIn)
        {
            MessageLabel.Text = "Please log in to create an event.";
            MessageLabel.TextColor = Colors.Red;
            return;
        }

        // Retrieve user input.
        string title = TitleEntry.Text?.Trim() ?? "";
        string location = LocationEntry.Text?.Trim() ?? "";
        string description = DescriptionEditor.Text?.Trim() ?? "";

        // Validate required text fields.
        if (string.IsNullOrWhiteSpace(title) ||
            string.IsNullOrWhiteSpace(location) ||
            string.IsNullOrWhiteSpace(description))
        {
            MessageLabel.Text = "All fields are required.";
            MessageLabel.TextColor = Colors.Red;
            return;
        }

        // Validate event visibility.
        if (VisibilityPicker.SelectedIndex == -1)
        {
            MessageLabel.Text = "Please select event visibility.";
            MessageLabel.TextColor = Colors.Red;
            return;
        }

        // Combine the selected date and time.
        DateTime eventDateTime =
            (EventDatePicker.Date ?? DateTime.Today).Date
            + (EventTimePicker.Time ?? TimeSpan.Zero);

        // Prevent events from being scheduled in the past.
        if (eventDateTime <= DateTime.Now)
        {
            MessageLabel.Text =
                "Event must be scheduled for a future date and time.";
            MessageLabel.TextColor = Colors.Red;
            return;
        }

        // Create an event using the validated form data.
        var newEvent = new Linqd.App.Models.Event
        {
            Title = title,
            Location = location,
            Description = description,
            StartDateTime = eventDateTime,
            Visibility = VisibilityPicker.SelectedItem?.ToString() ?? "Public",
            HostUserId = _sessionService.CurrentUser!.UserId
        };

        try
        {
            await _eventService.AddEventAsync(newEvent);

            MessageLabel.Text = "Event created successfully!";
            MessageLabel.TextColor = Colors.Green;
        }
        catch (Exception ex)
        {
            MessageLabel.Text = "Unable to save event. Please try again.";
            MessageLabel.TextColor = Colors.Red;

            System.Diagnostics.Debug.WriteLine(
                $"Event save failed: {ex}");
        }
    }
}
