namespace BLEExample.Services.Navigation
{
    /// <summary>
    /// Abstracts Shell navigation for simplification of navigating the view heirachy
    /// All views must still be registered with Routing.RegisterRoute("exampleview, typeof(ExampleView))
    /// Found in <see cref="AppShell.xaml"/>
    /// </summary>
    public class NavigationService : INavigationService
    {
        /// <summary>
        /// Initalizes the application to the default start view
        /// </summary>
        public Task InitializeAsync() => NavigateToAsync("//BLEScan"); 

        /// <summary>
        /// Routes the appliation to a specific view using Shell navigations routing 
        /// <see href="https://learn.microsoft.com/en-us/dotnet/maui/fundamentals/shell/navigation"/>
        /// </summary>
        /// <param name="route">Defines the path to content that exists as part of the Shell visual hierarchy
        /// <see href="https://learn.microsoft.com/en-us/dotnet/maui/fundamentals/shell/navigation#routes"/></param>
        /// <param name="routeParameters">Parameters may be passed to the view model using a Dictionary
        /// <see href="https://learn.microsoft.com/en-us/dotnet/maui/fundamentals/shell/navigation#process-navigation-data-using-a-single-method"/></param>
        public Task NavigateToAsync(string route, IDictionary<string, object> routeParameters = null)
        {
            var shellNavigation = new ShellNavigationState(route);

            return routeParameters != null
                ? Shell.Current.GoToAsync(shellNavigation, routeParameters)
                : Shell.Current.GoToAsync(shellNavigation);
        }

        /// <summary>
        /// Uses Shell's GoToAsync to remove one view from the navigation stack
        /// </summary>
        public Task PopAsync() =>
            Shell.Current.GoToAsync("..");
    }
}
