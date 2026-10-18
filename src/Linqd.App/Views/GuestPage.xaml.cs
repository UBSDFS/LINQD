
namespace Linqd.App.Views;

public partial class GuestPage : ContentPage
{
    public GuestPage()
    {
        InitializeComponent();
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
