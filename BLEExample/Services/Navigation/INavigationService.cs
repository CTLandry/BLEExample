using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLEExample.Services.Navigation
{
    /// <summary>
    /// Definition of Navigation services
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// Routes to the default view upon launch
        /// </summary>
        Task InitializeAsync();

        /// <summary>
        /// Navigates to the target view
        /// </summary>
        /// <param name="route"></param>
        /// <param name="routeParameters"></param>
        Task NavigateToAsync(string route, IDictionary<string, object> routeParameters = null);

        /// <summary>
        /// Pops one view from the view stack
        /// </summary>
        Task PopAsync();
    }
}
