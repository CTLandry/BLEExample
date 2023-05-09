
namespace BLEExample.Services.BLE.SharedImplementation
{
    /// <summary>
    /// Android allows for connection priority
    /// https://developer.android.com/reference/android/bluetooth/BluetoothGatt
    /// </summary>
    public enum BLEConnectionIntervals
    {
        /// <summary>
        /// Normal Priority
        /// </summary>
        Normal = 0,
        /// <summary>
        /// High Priority
        /// </summary>
        High = 1,
        /// <summary>
        /// Low Priority
        /// </summary>
        Low = 2

    }
}
