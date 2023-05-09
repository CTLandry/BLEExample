using BLEExample.Services.BLE.SharedImplementation.Contracts;

namespace BLEExample.Services.BLE.SharedImplementation.EventArgs
{
    /// <summary>
    /// Object for holding device args when scanning or connecting to BLE peripherals
    /// </summary>
    public class BLEPeripheralEventArgs : System.EventArgs
    {
        /// <summary>
        /// The peripheral
        /// </summary>
        public IBLEPeripheral Peripheral;
    }
}
