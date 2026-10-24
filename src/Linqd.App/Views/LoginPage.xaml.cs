
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

        // Retrieve shared services registered in MauiProgram.cs.
        _authService = IPlatformApplication.Current!
            .Services.GetRequiredService<AuthenticationService>();

        _sessionService = IPlatformApplication.Current!
            .Services.GetRequiredService<UserSessionService>();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string username = UsernameEntry.Text?.Trim() ?? "";
        string password = PasswordEntry.Text ?? "";

        // Validate required fields.
        if (string.IsNullOrWhiteSpace(username) ||
            string.IsNullOrWhiteSpace(password))
        {
            MessageLabel.Text = "Enter your username and password.";
            MessageLabel.IsVisible = true;
            return;
        }

        User? user;

        try
        {
            // Authenticate using the database-backed service.
            user = await _authService.LoginAsync(username, password);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(
                $"Login failed: {ex}");

            MessageLabel.Text = "Unable to log in. Please try again.";
            MessageLabel.IsVisible = true;
            return;
        }

        // Reject invalid credentials.
        if (user == null)
        {
            MessageLabel.Text = "Invalid username or password.";
            MessageLabel.IsVisible = true;
            return;
        }

        // Store the authenticated user in the shared session.
        _sessionService.Login(user);

        MessageLabel.IsVisible = false;

        await DisplayAlertAsync(
            "Login Successful",
            $"Welcome, {user.FirstName}!",
            "OK");

        // Navigate to the Home tab.
        await Shell.Current.GoToAsync("//MainTabs/HomeTab");
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
