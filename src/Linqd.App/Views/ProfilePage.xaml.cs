
using Linqd.App.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Linqd.App.Views;

public partial class ProfilePage : ContentPage
{
    private readonly UserSessionService _sessionService;

    public ProfilePage()
    {
        InitializeComponent();

        _sessionService = IPlatformApplication.Current!
            .Services.GetRequiredService<UserSessionService>();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        var user = _sessionService.CurrentUser;

        if (user != null)
        {
            NameLabel.Text = $"Name: {user.FirstName} {user.LastName}";
            UsernameLabel.Text = $"Username: {user.Username}";
            EmailLabel.Text = $"Email: {user.Email}";
            LogoutButton.IsVisible = true;
        }
        else
        {
            NameLabel.Text = "Browsing as Guest";
            UsernameLabel.Text = "";
            EmailLabel.Text = "";
            LogoutButton.IsVisible = false;
        }
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        _sessionService.Logout();
        await Shell.Current.GoToAsync("//WelcomePage");
    }
}
