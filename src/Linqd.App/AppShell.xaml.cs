
using Linqd.App.Views;

namespace Linqd.App;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(
			"login",
			typeof(LoginPage));
		Routing.RegisterRoute(
			"register",
			typeof(RegisterPage));
		Routing.RegisterRoute(
			"guest",
			typeof(GuestPage));
		Routing.RegisterRoute(
			"home",
			typeof(HomePage));
		Routing.RegisterRoute(
			"profile",
			typeof(ProfilePage));
		Routing.RegisterRoute(
"eventdetails",
typeof(EventDetailsPage));
		Routing.RegisterRoute(
	"rsvp",
	typeof(RsvpPage));
	}
}
