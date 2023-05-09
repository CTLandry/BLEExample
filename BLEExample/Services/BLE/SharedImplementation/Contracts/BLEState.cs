
namespace BLEExample.Services.BLE.SharedImplementation.Contracts
{
    /// <summary>
    /// State of the blue tooth connection
    /// </summary>
    public enum BLEState
    {
        Unknown,
        Unavailable,
        Unauthorized,
        TurningOn,
        On,
        TurningOff,
        Off
    }
}
