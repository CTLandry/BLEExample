using BLEExample.Services.BLE.SharedImplementation;
using BLEExample.Services.BLE.SharedImplementation.Contracts;
using BLEExample.Services.BLE.SharedImplementation.EventArgs;

namespace BLEExample.Services.BLE.Interaction
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

        /// <summary>
        /// Start the BLE scan
        /// </summary>
        /// <param name="allowDuplicatesKey"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StartScanningForDevicesAsync(bool allowDuplicatesKey = false,
                                                CancellationToken cancellationToken = default);

        /// <summary>
        /// Stop the scan
        /// </summary>
        public void StopScanningForDevicesAsync();

        /// <summary>
        /// Connect to the target ble peripheral
        /// </summary>
        /// <param name="peripheral"></param>
        /// <param name="connectParameters"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task ConnectToPeripheralAsync(IBLEPeripheral peripheral, 
            BLEConnectionParameters connectParameters = default, CancellationToken cancellationToken = default);

        /// <summary>
        /// Disconnect from a connected peripheral
        /// </summary>
        /// <param name="peripheral"></param>
        /// <returns></returns>
        public Task DisconnectPeripheralAsync(IBLEPeripheral peripheral);
    }
}
