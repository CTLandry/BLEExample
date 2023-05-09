
namespace BLEExample.Services.BLE.SharedImplementation.EventArgs
{
    /// <summary>
    /// Definition of state changed event for BLE
    /// </summary>
    public class BLEStateChangedArgs : System.EventArgs
    {
        /// <summary>
        /// State before the change.
        /// </summary>
        public BLEPeripheralConnectionState PreviousState { get; }

        /// <summary>
        /// Current state.
        /// </summary>
        public BLEPeripheralConnectionState NewState { get; }

        public BLEStateChangedArgs(BLEPeripheralConnectionState previousState, BLEPeripheralConnectionState newState)
        {
            PreviousState = previousState;
            NewState = newState;
        }
    }
}
