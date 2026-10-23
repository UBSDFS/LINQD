
using Linqd.App.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Linqd.App.Views;

public partial class HomePage : ContentPage
{
    private readonly EventService _eventService;
    private readonly UserSessionService _sessionService;

    public HomePage()
    {
        InitializeComponent();

        // Retrieve shared services.
        _eventService = IPlatformApplication.Current!
            .Services.GetRequiredService<EventService>();

        _sessionService = IPlatformApplication.Current!
            .Services.GetRequiredService<UserSessionService>();
    }

    // Refresh events whenever Home becomes visible.
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        try
        {
            await LoadEventsAsync();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(
                $"Event loading failed: {ex}");
        }
    }

    private async Task LoadEventsAsync()
    {
        // Clear previous event displays.
        PublicEventsContainer.Children.Clear();
        PrivateEventsContainer.Children.Clear();

        // Retrieve saved events.
        var events = await _eventService.GetEventsAsync();

        // Public events are available to everyone.
        var publicEvents = events
            .Where(eventItem => eventItem.Visibility == "Public")
            .OrderBy(eventItem => eventItem.StartDateTime)
            .ToList();

        // Private events are visible only to their creator for now.
        var privateEvents = events
            .Where(eventItem =>
                eventItem.Visibility == "Private" &&
                _sessionService.IsLoggedIn &&
                eventItem.HostUserId ==
                    _sessionService.CurrentUser?.UserId)
            .OrderBy(eventItem => eventItem.StartDateTime)
            .ToList();

        // Show or hide empty-state messages.
        NoPublicEventsLabel.IsVisible = publicEvents.Count == 0;

        NoPrivateEventsLabel.IsVisible = privateEvents.Count == 0;

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

    // Build a simple visual display for each event.
    private View CreateEventDisplay(Linqd.App.Models.Event eventItem)
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

        return new Border
        {
            StrokeThickness = 1,
            Padding = new Thickness(5),
            Content = eventLayout
        };
    }

    // Preserve existing Profile navigation.
    private async void OnProfileClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainTabs/ProfileTab");
    }
}
