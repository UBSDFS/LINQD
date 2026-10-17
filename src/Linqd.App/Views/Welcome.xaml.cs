
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

    private void OnRegisterClicked(object sender, EventArgs e)
    {
        // Registration navigation will be added later.
    }

    private void OnGuestClicked(object sender, EventArgs e)
    {
        // Guest navigation will be added later.
    }
}
