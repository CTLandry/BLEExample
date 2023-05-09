
namespace BLEExample.Services.BLE.SharedImplementation
{
    /// <summary>
    /// Defines a parameters used in connecting to BLE peripherals.
    /// </summary>
    public class BLEConnectionParameters
    {
        /// <summary>
        /// Android only, from documentation:  
        /// boolean: Whether to directly connect to the remote device (false) or to automatically connect as soon as the remote device becomes available (true).
        /// </summary>
        public bool AutoConnect { get; }

        /// <summary>
        /// Android only: For Dual Mode device, force transport mode to LE. The default is false.
        /// </summary>
        public bool ForceBleTransport { get; }
        public BLEConnectionParameters(bool autoConnect, bool forceBleTransport) 
        { 
            AutoConnect = autoConnect;
            ForceBleTransport = forceBleTransport;
        }
       
    }
}
