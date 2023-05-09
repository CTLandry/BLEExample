

namespace BLEExample.Services.Settings
{
    /// <summary>
    /// Use preferences API to store key value pairs related to settings
    /// </summary>
    public class SettingsService : ISettingsService
    {
        private const string IdUseMocks = "use_mocks";
        private readonly bool UseMocksDefault = true;
        /// <summary>
        /// Whether or not the application should use mock implementations
        /// </summary>
        public bool UseMocks
        {
            get => Preferences.Get(IdUseMocks, UseMocksDefault);
            set => Preferences.Set(IdUseMocks, value);
        }
    }
}
