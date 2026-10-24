
using Linqd.App.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Linqd.App.Views;

public partial class HomePage : ContentPage
{
    private readonly EventService _eventService;
    private readonly UserSessionService _sessionService;

    // Tracks which event list the user selected.
    private string _selectedEventFilter = "All";

    public HomePage()
    {
        InitializeComponent();

        _eventService = IPlatformApplication.Current!
            .Services.GetRequiredService<EventService>();

        _sessionService = IPlatformApplication.Current!
            .Services.GetRequiredService<UserSessionService>();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await RefreshEventsAsync();
    }

    private async Task LoadEventsAsync()
    {
        // Clear previous event displays.
        PublicEventsContainer.Children.Clear();
        PrivateEventsContainer.Children.Clear();

        List<Linqd.App.Models.Event> events;

        // Retrieve events based on the selected filter.
        if (_selectedEventFilter == "Hosting")
        {
            if (!_sessionService.IsLoggedIn ||
                _sessionService.CurrentUser == null)
            {
                ShowEmptyMessage(
                    "Log in to view events you're hosting.");
                return;
            }

            events = await _eventService.GetHostedEventsAsync(
                _sessionService.CurrentUser.UserId);
        }
        else if (_selectedEventFilter == "Attending")
        {
            ShowEmptyMessage(
                "Attending events will appear after you RSVP.");
            return;
        }
        else
        {
            events = await _eventService.GetEventsAsync();
        }

        // Public events are available to everyone.
        var publicEvents = events
            .Where(eventItem =>
                eventItem.Visibility == "Public")
            .OrderBy(eventItem => eventItem.StartDateTime)
            .ToList();

        // Private events are visible only to their creator.
        var privateEvents = events
            .Where(eventItem =>
                eventItem.Visibility == "Private" &&
                _sessionService.IsLoggedIn &&
                eventItem.HostUserId ==
                    _sessionService.CurrentUser?.UserId)
            .OrderBy(eventItem => eventItem.StartDateTime)
            .ToList();

        // Set empty-state messages.
        NoPublicEventsLabel.Text =
            _selectedEventFilter == "Hosting"
                ? "No public events you're hosting."
                : "No public events available.";

        NoPrivateEventsLabel.Text =
            _selectedEventFilter == "Hosting"
                ? "No private events you're hosting."
                : "No private plans available.";

        NoPublicEventsLabel.IsVisible =
            publicEvents.Count == 0;

        NoPrivateEventsLabel.IsVisible =
            privateEvents.Count == 0;

        // Display public events.
        foreach (var eventItem in publicEvents)
        {
            PublicEventsContainer.Children.Add(
                CreateEventDisplay(eventItem));
        }

        // Display private events.
        foreach (var eventItem in privateEvents)
        {
            PrivateEventsContainer.Children.Add(
                CreateEventDisplay(eventItem));
        }
    }

    private void ShowEmptyMessage(string message)
    {
        NoPublicEventsLabel.Text = message;
        NoPublicEventsLabel.IsVisible = true;

        NoPrivateEventsLabel.IsVisible = false;
    }

    // Build a simple visual display for each event.
    private View CreateEventDisplay(
        Linqd.App.Models.Event eventItem)
    {
        var eventLayout = new VerticalStackLayout
        {
            Spacing = 4,
            Padding = new Thickness(10)
        };

        eventLayout.Children.Add(new Label
        {
            Text = eventItem.Title,
            FontSize = 18,
            FontAttributes = FontAttributes.Bold
        });

        eventLayout.Children.Add(new Label
        {
            Text = eventItem.StartDateTime.ToString(
                "MMM dd, yyyy - h:mm tt")
        });

        eventLayout.Children.Add(new Label
        {
            Text = eventItem.Location
        });
        var tapGesture = new TapGestureRecognizer();

        tapGesture.Tapped += async (sender, args) =>
        {
            await Shell.Current.GoToAsync(
                $"eventdetails?eventId={eventItem.EventId}");
        };
        var eventBorder = new Border
        {
            StrokeThickness = 1,
            Padding = new Thickness(5),
            Content = eventLayout
        };

        eventBorder.GestureRecognizers.Add(tapGesture);

        return eventBorder;
    }

    // Event filter buttons.
    private async void OnAllEventsClicked(
        object sender, EventArgs e)
    {
        _selectedEventFilter = "All";
        await RefreshEventsAsync();
    }

    private async void OnAttendingEventsClicked(
        object sender, EventArgs e)
    {
        _selectedEventFilter = "Attending";
        await RefreshEventsAsync();
    }

    private async void OnHostingEventsClicked(
        object sender, EventArgs e)
    {
        _selectedEventFilter = "Hosting";
        await RefreshEventsAsync();
    }

    private async Task RefreshEventsAsync()
    {
        try
        {
            await LoadEventsAsync();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(
                $"Event loading failed: {ex}");

            await DisplayAlertAsync(
                "Error",
                "Unable to load events.",
                "OK");
        }
    }

    // Preserve existing Profile navigation.
    private async void OnProfileClicked(
        object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(
            "//MainTabs/ProfileTab");
    }
}
