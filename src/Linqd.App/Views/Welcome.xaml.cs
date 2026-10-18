using Linqd.App.Services;
using Microsoft.Extensions.DependencyInjection;
namespace Linqd.App.Views;

public partial class WelcomePage : ContentPage
{
    public WelcomePage()
    {
        InitializeComponent();
    }


    private async void OnLoginClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("login");
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("register");
    }

    private async void OnGuestClicked(object sender, EventArgs e)
    {
        // Clear any previous logged-in session.
        var sessionService = IPlatformApplication.Current!
            .Services.GetRequiredService<UserSessionService>();

        sessionService.Logout();

        // Navigate to the guest experience.
        await Shell.Current.GoToAsync("guest");
    }
}
