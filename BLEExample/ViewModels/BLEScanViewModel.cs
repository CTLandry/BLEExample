using BLEExample.Services.BLE.SharedImplementation.Contracts;
using System.Collections.ObjectModel;
using BLEExample.Services.BLE.SharedImplementation.EventArgs;
using BLEExample.Services.BLE.Interaction;

namespace BLEExample.ViewModels
{
    public class BLEScanViewModel : ViewModel
    {
        private readonly IBLEHandler _bleHandlerService;

        public ObservableCollection<IBLEPeripheral> BLEPeripherals { get; set; } = new ObservableCollection<IBLEPeripheral>();
        public BLEScanViewModel(IBLEHandler bleHandlerService) 
        {
            _bleHandlerService = bleHandlerService;
            _bleHandlerService.PeripheralDiscovered += PeripheralDiscovered;
            _bleHandlerService.PeripheralConnectionError += PeripheralConnectionError;
            _bleHandlerService.PeripheralConnectionLost += PeripheralConnectionLost;
            _bleHandlerService.PeripheralConnected += PeripheralConnected;
            _bleHandlerService.PeripheralAdvertised += PeripheralAdvertised;
          
        }

        private void PeripheralAdvertised(object sender, BLEPeripheralEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void PeripheralConnected(object sender, BLEPeripheralEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void PeripheralConnectionLost(object sender, BLEPeripheralErrorEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void PeripheralConnectionError(object sender, BLEPeripheralErrorEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void PeripheralDiscovered(object sender, BLEPeripheralEventArgs e)
        {
            throw new NotImplementedException();
        }

        private async Task ScanForDevices()
        {

        }

        public override void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            
        }
    }
}
