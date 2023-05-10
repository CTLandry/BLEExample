using Android.Bluetooth;
using BLEExample.Services.BLE.SharedImplementation.Contracts;


namespace BLEExample.Platforms.Android.Services.BLE.PlatformImplementation.Extentions
{
    /// <summary>
    /// Allows conversion of native states to the shared states of BLE connections
    /// </summary>
    public static class BLEStateExtension
    {
        public static BLEState ToBLEState(this State state)
        {
            switch (state)
            {
                case State.Connected:
                case State.Connecting:
                case State.Disconnected:
                case State.Disconnecting:
                    return BLEState.On;
                case State.Off:
                    return BLEState.Off;
                case State.On:
                    return BLEState.On;
                case State.TurningOff:
                    return BLEState.TurningOff;
                case State.TurningOn:
                    return BLEState.TurningOn;
                default:
                    return BLEState.Unknown;
            }
        }
    }
}
