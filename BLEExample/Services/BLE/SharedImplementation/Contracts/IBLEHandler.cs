using BLEExample.Services.BLE.SharedImplementation.EventArgs;

namespace BLEExample.Services.BLE.SharedImplementation.Contracts
{
    /// <summary>
    /// Definition of interactions with BLE
    /// </summary>
    public interface IBLEHandler
    {
        /// <summary>
        /// Invoked when the advertisement is received.
        /// </summary>
        event EventHandler<BLEPeripheralEventArgs> PeripheralAdvertised;
        /// <summary>
        /// Invoked when an advertisement is recevied for the first time.
        /// </summary>
        event EventHandler<BLEPeripheralEventArgs> PeripheralDiscovered;
        /// <summary>
        /// Invoked when a Peripheral has been connected.
        /// </summary>
        event EventHandler<BLEPeripheralEventArgs> PeripheralConnected;
        /// <summary>
        /// Invoked when a Peripheral has been disconnected. This occurs on unintended disconnects (e.g. when the Peripheral exploded).
        /// </summary>
        event EventHandler<BLEPeripheralErrorEventArgs> PeripheralConnectionLost;
        /// <summary>
        /// Invoked when the connection to a Peripheral fails.
        /// </summary>
        event EventHandler<BLEPeripheralErrorEventArgs> PeripheralConnectionError;
        /// <summary>
        /// Invoked when the scan has been stopped due the timeout after <see cref="ScanTimeout"/> ms.
        /// </summary>
        event EventHandler ScanTimeoutElapsed;

        /// <summary>
        /// The handler is or is not currently scanning for Peripherals.
        /// </summary>
        bool IsScanning { get; }
    }
}
