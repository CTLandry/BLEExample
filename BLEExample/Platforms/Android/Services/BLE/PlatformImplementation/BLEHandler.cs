using Android.Bluetooth;
using Android.Bluetooth.LE;
using Android.Content;
using BLEExample.Models;
using BLEExample.Services.BLE.SharedImplementation;
using BLEExample.Services.BLE.SharedImplementation.Contracts;

namespace BLEExample.Services.BLE.Interaction
{
    /// <summary>
    /// Native android implementation of the BLEHandler
    /// </summary>
    public class BLEHandler : BLEHandlerBase, IBLEHandler
    {
        private readonly BluetoothManager _bluetoothManager;
        //private readonly BluetoothAdapter _bluetoothAdapter;
        private readonly BleScanCallback _scanCallback;

        public BLEHandler()
        {
            _bluetoothManager = (BluetoothManager)MauiApplication.Current.GetSystemService(Context.BluetoothService);
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
                if (_bluetoothManager.Adapter.BluetoothLeScanner != null)
                {
                    _bluetoothManager.Adapter.BluetoothLeScanner.StartScan(_scanCallback);
                }
                return Task.FromResult(true);
            }
            catch (Exception ex)
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
                    
                var peripheral = new BLEPeripheral(Guid.NewGuid(), result.ScanRecord.DeviceName, result.Device, 
                                                    BLEPeripheralConnectionState.Disconnected, result.Rssi, result.ScanRecord.ServiceSolicitationUuids?.ToList());

                _bleHandler.HandleDiscoveredPeripheral(peripheral);

            }
        }
    }
}
