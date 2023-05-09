using BLEExample.Services.Navigation;
using BLEExample.Views;

namespace BLEExample;

public partial class AppShell : Shell
{
	private readonly INavigationService _navigationService;
	public AppShell(INavigationService navigationService)
	{
		_navigationService = navigationService;
		InitializeRouting();
		InitializeComponent();
	}

    protected override async void OnHandlerChanged()
    {
        base.OnHandlerChanged();

        if (Handler is not null)
        {
            await _navigationService.InitializeAsync();
        }
    }

    private static void InitializeRouting()
    {
        Routing.RegisterRoute("BLEScan", typeof(BLEScanView));
    }
}
