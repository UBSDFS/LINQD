
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
	}
}
