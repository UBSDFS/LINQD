
using Linqd.App.Models;
using Linqd.App.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Linqd.App.Views;

[QueryProperty(nameof(EventId), "eventId")]
public partial class RsvpPage : ContentPage
{
    private readonly RsvpService _rsvpService;
    private readonly EventService _eventService;
    private readonly UserSessionService _sessionService;

    private int _eventId;
    private bool _isSubmitting;

    public string EventId
    {
        get => _eventId.ToString();
        set
        {
            if (int.TryParse(value, out int parsedId))
            {
                _eventId = parsedId;
            }
        }
    }

    public RsvpPage()
    {
        InitializeComponent();

        var services = IPlatformApplication.Current!.Services;

        _rsvpService =
            services.GetRequiredService<RsvpService>();

        _eventService =
            services.GetRequiredService<EventService>();

        _sessionService =
            services.GetRequiredService<UserSessionService>();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        SubmitRsvpButton.IsEnabled = false;

        try
        {
            var selectedEvent =
                await _eventService.GetEventByIdAsync(_eventId);

            if (selectedEvent == null)
            {
                EventTitleLabel.Text = "Event not found.";
                return;
            }

            EventTitleLabel.Text = selectedEvent.Title;

            if (_sessionService.IsLoggedIn &&
                _sessionService.CurrentUser != null)
            {
                var user = _sessionService.CurrentUser;

                FirstNameEntry.Text = user.FirstName;
                LastNameEntry.Text = user.LastName;
                EmailEntry.Text = user.Email;
            }
            else
            {
                FirstNameEntry.Text = string.Empty;
                LastNameEntry.Text = string.Empty;
                EmailEntry.Text = string.Empty;
            }

            ResponsePicker.SelectedIndex = -1;
            SubmitRsvpButton.IsEnabled = true;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(
                $"RSVP page loading failed: {ex}");

            await DisplayAlertAsync(
                "Error",
                "Unable to load RSVP information.",
                "OK");
        }
    }

    private async void OnSubmitRsvpClicked(
        object sender, EventArgs e)
    {
        if (_isSubmitting)
            return;

        string firstName =
            FirstNameEntry.Text?.Trim() ?? string.Empty;

        string lastName =
            LastNameEntry.Text?.Trim() ?? string.Empty;

        string email =
            EmailEntry.Text?.Trim() ?? string.Empty;

        string response =
            ResponsePicker.SelectedItem?.ToString()
            ?? string.Empty;

        if (string.IsNullOrWhiteSpace(firstName) ||
            string.IsNullOrWhiteSpace(lastName) ||
            string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(response))
        {
            await DisplayAlertAsync(
                "Missing Information",
                "Please complete all fields.",
                "OK");
            return;
        }

        if (!System.Net.Mail.MailAddress.TryCreate(
                email, out var parsedEmail) ||
            parsedEmail.Address != email)
        {
            await DisplayAlertAsync(
                "Invalid Email",
                "Please enter a valid email address.",
                "OK");
            return;
        }

        _isSubmitting = true;
        SubmitRsvpButton.IsEnabled = false;

        try
        {
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

            int? userId = _sessionService.IsLoggedIn
                ? _sessionService.CurrentUser?.UserId
                : null;

            // Prevent duplicate RSVPs for logged-in users.
            if (userId.HasValue)
            {
                var existingRsvp =
                    await _rsvpService.GetRsvpByUserAndEventAsync(
                        userId.Value, _eventId);

                if (existingRsvp != null)
                {
                    await DisplayAlertAsync(
                        "Already Responded",
                        "You have already RSVP'd to this event.",
                        "OK");
                    return;
                }
            }

            var newRsvp = new Rsvp
            {
                EventId = _eventId,
                UserId = userId,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Response = response,
                CreatedAt = DateTime.UtcNow
            };

            await _rsvpService.AddRsvpAsync(newRsvp);

            await DisplayAlertAsync(
                "RSVP Saved",
                "Your response has been saved successfully.",
                "OK");

            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(
                $"RSVP submission failed: {ex}");

            await DisplayAlertAsync(
                "Error",
                "Unable to save your RSVP.",
                "OK");
        }
        finally
        {
            _isSubmitting = false;
            SubmitRsvpButton.IsEnabled = true;
        }
    }

    private async void OnCancelClicked(
        object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
