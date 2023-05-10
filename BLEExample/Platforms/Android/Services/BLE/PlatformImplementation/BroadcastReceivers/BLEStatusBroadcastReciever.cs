
using Android.Bluetooth;
using Android.Content;
using BLEExample.Platforms.Android.Services.BLE.PlatformImplementation.Extentions;
using BLEExample.Services.BLE.SharedImplementation.Contracts;

namespace BLEExample.Platforms.Android.Services.BLE.PlatformImplementation.BroadcastReceivers
{
    public class BLEStatusBroadcastReciever : BroadcastReceiver
    {
        private readonly Action<BLEState> _stateChangedHandler;

        public BLEStatusBroadcastReciever(Action<BLEState> stateChangedHandler)
        {
            _stateChangedHandler = stateChangedHandler;
        }

        public override void OnReceive(Context context, Intent intent)
        {
            var action = intent.Action;

            if (action != BluetoothAdapter.ActionStateChanged)
                return;

            var state = intent.GetIntExtra(BluetoothAdapter.ExtraState, -1);

            if (state == -1)
            {
                _stateChangedHandler?.Invoke(BLEState.Unknown);
                return;
            }

            var btState = (State)state;
            _stateChangedHandler?.Invoke(btState.ToBLEState());
        }
    }
}

