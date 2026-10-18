
using Linqd.App.Models;
using Linqd.App.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Linqd.App.Views;

public partial class LoginPage : ContentPage
{
    private readonly AuthenticationService _authService;
    private readonly UserSessionService _sessionService;

    public LoginPage()
    {
        InitializeComponent();

        // Retrieve the shared services registered in MauiProgram.cs.
        _authService = IPlatformApplication.Current!
            .Services.GetRequiredService<AuthenticationService>();

        _sessionService = IPlatformApplication.Current!
            .Services.GetRequiredService<UserSessionService>();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string username = UsernameEntry.Text?.Trim() ?? "";
        string password = PasswordEntry.Text ?? "";

        // Check for empty fields.
        if (string.IsNullOrWhiteSpace(username) ||
            string.IsNullOrWhiteSpace(password))
        {
            MessageLabel.Text = "Enter your username and password.";
            MessageLabel.IsVisible = true;
            return;
        }

        // Validate the test credentials.
        if (!_authService.ValidateLogin(username, password))
        {
            MessageLabel.Text = "Invalid username or password.";
            MessageLabel.IsVisible = true;
            return;
        }

        // Create the logged-in user.

        User? user = _authService.GetUser(username);

        if (user == null)
        {
            MessageLabel.Text = "Unable to retrieve user profile.";
            MessageLabel.IsVisible = true;
            return;
        }

        // Save the user in the shared session.
        _sessionService.Login(user);

        MessageLabel.IsVisible = false;

        await DisplayAlertAsync(
            "Login Successful",
            $"Welcome, {user.FirstName}!",
            "OK");
        await Shell.Current.GoToAsync("//MainTabs/HomeTab");
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
