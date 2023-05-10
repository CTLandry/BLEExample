
using BLEExample.Services.BLE.SharedImplementation;
using BLEExample.Services.BLE.SharedImplementation.Contracts;

namespace BLEExample.Services.BLE.Interaction
{
    /// <summary>
    /// iOS Implementation of BLE Handler
    /// </summary>
    public class BLEHandler : BLEHandlerBase, IBLEHandler
    {
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
            throw new NotImplementedException();
        }

        protected override void StopScanningNative()
        {
            throw new NotImplementedException();
        }
    }
}
