namespace BLEExample.Services.Settings
{
    /// <summary>
    /// Service for handling environment settings
    /// </summary>
    public interface ISettingsService
    {
        /// <summary>
        /// Used to determine if run time environment is intended for mock implementations
        /// </summary>
        bool UseMocks { get; set; }
    }
}
