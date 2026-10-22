using Linqd.App.Data;

namespace Linqd.App;

public partial class App : Application
{
	private readonly LinqdDatabase _database;

	public App(LinqdDatabase database)
	{
		InitializeComponent();

		_database = database;
	}

	protected override Window CreateWindow(
		IActivationState? activationState)
	{
		var window = new Window(new AppShell());

		window.Created += async (sender, args) =>
		{
			try
			{
				await _database.InitializeAsync();
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(
					$"Database initialization failed: {ex.Message}"
				);
			}
		};

		return window;
	}
}