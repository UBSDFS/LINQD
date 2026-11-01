
using System.Text.Json;
using Linqd.App.Models;
using Linqd.App.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Linqd.App.Views;

public partial class DiscoverPage : ContentPage
{
    private readonly ExternalEventService _eventService;
    private bool _isLoading;

    public DiscoverPage()
    {
        InitializeComponent();

        _eventService = IPlatformApplication.Current!
            .Services.GetRequiredService<ExternalEventService>();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await LoadExternalEventsAsync();
    }

    private async Task LoadExternalEventsAsync()
    {
        if (_isLoading)
            return;

        _isLoading = true;

        LoadingIndicator.IsVisible = true;
        LoadingIndicator.IsRunning = true;

        EmptyEventsLabel.IsVisible = false;
        DiscoverEventsContainer.Children.Clear();

        try
        {
            // Read the locally configured API key.
            using var stream =
                await FileSystem.OpenAppPackageFileAsync(
                    "ticketmaster.local.json");

            using var document =
                await JsonDocument.ParseAsync(stream);

            string apiKey = document.RootElement
                .GetProperty("TicketmasterApiKey")
                .GetString() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new InvalidOperationException(
                    "Ticketmaster API key is missing.");
            }

            // Retrieve public events in Norfolk.
            List<ExternalEvent> events =
                await _eventService.GetEventsAsync(
                    apiKey, "Norfolk");

            if (events.Count == 0)
            {
                EmptyEventsLabel.Text =
                    "No public events found.";
                EmptyEventsLabel.IsVisible = true;
                return;
            }

            foreach (var eventItem in events)
            {
                DiscoverEventsContainer.Children.Add(
                    CreateEventDisplay(eventItem));
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(
                $"Discover API error: {ex}");

            EmptyEventsLabel.Text =
                $"API Error: {ex.GetType().Name}\n{ex.Message}";

            EmptyEventsLabel.IsVisible = true;
        }
        finally
        {
            LoadingIndicator.IsRunning = false;
            LoadingIndicator.IsVisible = false;
            _isLoading = false;
        }
    }

    // Create a visual card for each external event.
    private View CreateEventDisplay(ExternalEvent eventItem)
    {
        var eventLayout = new VerticalStackLayout
        {
            Spacing = 6,
            Padding = new Thickness(12)
        };

        eventLayout.Children.Add(new Label
        {
            Text = eventItem.Title,
            FontSize = 18,
            FontAttributes = FontAttributes.Bold
        });

        eventLayout.Children.Add(new Label
        {
            Text = eventItem.StartDateTime?
                .ToString("MMM dd, yyyy - h:mm tt")
                ?? "Date to be announced",
            FontSize = 14
        });

        eventLayout.Children.Add(new Label
        {
            Text = string.IsNullOrWhiteSpace(
                eventItem.Location)
                ? "Location unavailable"
                : eventItem.Location,
            FontSize = 14
        });

        var eventBorder = new Border
        {
            StrokeThickness = 1,
            Padding = new Thickness(5),
            Content = eventLayout
        };

        // Open the official Ticketmaster event page.
        var tapGesture = new TapGestureRecognizer();

        tapGesture.Tapped += async (sender, args) =>
        {
            if (Uri.TryCreate(
                eventItem.EventUrl,
                UriKind.Absolute,
                out var eventUri) &&
                eventUri.Scheme == Uri.UriSchemeHttps)
            {
                await Launcher.Default.OpenAsync(eventUri);
            }
        };

        eventBorder.GestureRecognizers.Add(tapGesture);

        return eventBorder;
    }
}
