using Linqd.App.Models;
using Linqd.App.Services;
using Microsoft.Extensions.DependencyInjection;
namespace Linqd.App.Views;

public partial class RegisterPage : ContentPage
{
    public RegisterPage()
    {
        InitializeComponent();
    }


    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        MessageLabel.Text = "Register button clicked!";

        string firstName = FirstNameEntry.Text?.Trim() ?? "";
        string lastName = LastNameEntry.Text?.Trim() ?? "";
        string email = EmailEntry.Text?.Trim() ?? "";
        string username = UsernameEntry.Text?.Trim() ?? "";
        string password = PasswordEntry.Text ?? "";
        string confirmPassword = ConfirmPasswordEntry.Text ?? "";

        if (string.IsNullOrWhiteSpace(firstName) ||
            string.IsNullOrWhiteSpace(lastName) ||
            string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(username) ||
            string.IsNullOrWhiteSpace(password) ||
            string.IsNullOrWhiteSpace(confirmPassword))
        {
            MessageLabel.Text = "All fields are required.";
            MessageLabel.TextColor = Colors.Red;
            return;
        }

        if (!System.Net.Mail.MailAddress.TryCreate(email, out var emailAddress) ||
            emailAddress.Address != email ||
            !emailAddress.Host.Contains('.'))
        {
            MessageLabel.Text = "Please enter a valid email address.";
            MessageLabel.TextColor = Colors.Red;
            return;
        }

        if (password.Length < 8)
        {
            MessageLabel.Text = "Password must be at least 8 characters.";
            MessageLabel.TextColor = Colors.Red;
            return;
        }

        if (password != confirmPassword)
        {
            MessageLabel.Text = "Passwords do not match.";
            MessageLabel.TextColor = Colors.Red;
            return;
        }


        var authenticationService = IPlatformApplication.Current!
            .Services.GetRequiredService<AuthenticationService>();

        var newUser = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Username = username
        };

        bool registered = await authenticationService.RegisterUserAsync(
    newUser,
    password);

        if (!registered)
        {
            MessageLabel.Text = "Username or email is already registered.";
            MessageLabel.TextColor = Colors.Red;
            return;
        }

        MessageLabel.Text = "Account created successfully!";
        MessageLabel.TextColor = Colors.Green;

    }


    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
