using BLEExample.Services.Navigation;
using BLEExample.Services.Settings;

namespace BLEExample;

public partial class App : Application
{
    private readonly ISettingsService _settingsService;
    private readonly INavigationService _navigationService;
    public App(ISettingsService settingsService, INavigationService navigationService)
	{
		_settingsService = settingsService;
		_navigationService = navigationService;

		InitializeComponent();

		MainPage = new AppShell(navigationService);
	}

   
}
