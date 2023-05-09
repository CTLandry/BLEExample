
namespace BLEExample.Services.BLE.SharedImplementation.Contracts
{
    /// <summary>
    /// Definition of peripherals discovered over BLE
    /// </summary>
    public interface IBLEPeripheral
    {
        /// <summary>
        /// Id of the perhipheral.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Name of the discoverd ble peripheral.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the native perhipheral object reference. 
        /// Should be cast to the native type.
        /// </summary>
        /// <value>The native device.</value>
        object NativePeripheral { get; }

        /// <summary>
        /// State of the peripheral.
        /// </summary>
        BLEPeripheralConnectionState ConnectionState { get; }

        bool UpdateConnectionInterval(BLEConnectionIntervals interval);
    }
}
