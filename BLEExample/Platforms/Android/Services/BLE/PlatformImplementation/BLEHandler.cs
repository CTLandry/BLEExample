using Android.Bluetooth;
using Android.Bluetooth.LE;
using BLEExample.Models;
using BLEExample.Services.BLE.SharedImplementation;
using BLEExample.Services.BLE.SharedImplementation.Contracts;

namespace BLEExample.Platforms.Android.Services.BLE.PlatformImplementation
{
    /// <summary>
    /// Native android implementation of the BLEHandler
    /// </summary>
    public class BLEHandler : BLEHandlerBase
    {
        private readonly BluetoothManager _bluetoothManager;
        private readonly BluetoothAdapter _bluetoothAdapter;
        private readonly BleScanCallback _scanCallback;

        public BLEHandler(BluetoothManager blueToothManager)
        {
            _bluetoothManager = blueToothManager;
            _bluetoothAdapter = blueToothManager.Adapter;
            _scanCallback = new BleScanCallback(this);

        }

        protected override Task ConnectToPeripheralNativeAsync(IBLEPeripheral peripheral, BLEConnectionParameters connectParameters, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        protected override void DisconnectNative(IBLEPeripheral peripheral)
        {
            throw new NotImplementedException();
        }

        protected override Task StartScanningNativeAsync(bool allowDuplicatesKey, CancellationToken scanCancellationToken)
        {
            try
            {
                if (_bluetoothAdapter.BluetoothLeScanner != null)
                {
                    _bluetoothAdapter.BluetoothLeScanner.StartScan(_scanCallback);
                }
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(true);
                //TODO android native scan exception
            }
            
        }

        protected override void StopScanningNative()
        {
            throw new NotImplementedException();
        }

        public class BleScanCallback : ScanCallback
        {
            private readonly BLEHandler _bleHandler;
            public BleScanCallback(BLEHandler bleHandler)
            {
                _bleHandler = bleHandler;
            }

            public override void OnScanFailed(ScanFailure errorCode)
            {
                base.OnScanFailed(errorCode);
            }

            public override void OnScanResult(ScanCallbackType callbackType, ScanResult result)
            {
                base.OnScanResult(callbackType, result);

                var peripheral = new BLEPeripheral(Guid.NewGuid(), result.Device.Name, result.Device, BLEPeripheralConnectionState.Disconnected);

                _bleHandler.HandleDiscoveredPeripheral(peripheral);

            }
        }
    }
}
