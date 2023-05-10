using BLEExample.Services.BLE.SharedImplementation.Contracts;
using System.Collections.ObjectModel;
using BLEExample.Services.BLE.SharedImplementation.EventArgs;
using BLEExample.Services.BLE.Interaction;
using BLEExample.Services.Dialog;
using BLEExample.Services.ErrorHandling;
using BLEExample.Services.PermissionRequests;
using BLEExample.Services.Navigation;
using System.Windows.Input;

namespace BLEExample.ViewModels
{
    public class BLEScanViewModel : ViewModel
    {
        private readonly IBLEHandler _bleHandlerService;
        private readonly IDialogService _dialogService;
        private readonly IPermissionService _permissionService;

        public ObservableCollection<IBLEPeripheral> BLEPeripherals { get; set; } = new ObservableCollection<IBLEPeripheral>();
        public bool IsScanning = false;

        public ICommand ScanCommand { get; set; }
        public BLEScanViewModel(IBLEHandler bleHandlerService,
                                INavigationService navigationService, 
                                IDialogService dialogService, 
                                IErrorReportingService errorReportingService,
                                IPermissionService permissionService) 
                                : base(navigationService, errorReportingService) 
        {
            
            _bleHandlerService = bleHandlerService;
            _dialogService = dialogService;
            _permissionService = permissionService;

            ScanCommand = new Command(async () => await ScanForDevices());

            _bleHandlerService.PeripheralDiscovered += PeripheralDiscovered;
            _bleHandlerService.PeripheralConnectionError += PeripheralConnectionError;
            _bleHandlerService.PeripheralConnectionLost += PeripheralConnectionLost;
            _bleHandlerService.PeripheralConnected += PeripheralConnected;
            _bleHandlerService.PeripheralAdvertised += PeripheralAdvertised;
          
        }

        private void PeripheralAdvertised(object sender, BLEPeripheralEventArgs e)
        {
            
        }

        private void PeripheralConnected(object sender, BLEPeripheralEventArgs e)
        {
           
        }

        private void PeripheralConnectionLost(object sender, BLEPeripheralErrorEventArgs e)
        {
           
        }

        private void PeripheralConnectionError(object sender, BLEPeripheralErrorEventArgs e)
        {
           
        }

        private void PeripheralDiscovered(object sender, BLEPeripheralEventArgs e)
        {
            BLEPeripherals.Add(e.Peripheral);
        }

        private async Task ScanForDevices()
        {
            try
            {
                IsScanning = true;

                if (await _permissionService.CheckAndRequestLocationPermission() != PermissionStatus.Granted)
                {
                    return;
                }

                if (await _permissionService.CheckAndRequestBLEPermission() != PermissionStatus.Granted)
                {
                    return;
                }
                BLEPeripherals.Clear();
                await _bleHandlerService.StartScanningForDevicesAsync();
            }
            catch (Exception ex)
            {
                //TODO
            }
            finally
            {
                IsScanning = false;
            }
            
        }

        public override void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            
        }
    }
}
