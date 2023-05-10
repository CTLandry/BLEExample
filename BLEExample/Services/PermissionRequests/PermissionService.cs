
using BLEExample.Services.Dialog;

namespace BLEExample.Services.PermissionRequests
{
    /// <summary>
    /// A service that helps with the boilerplate around using 
    /// Microsoft.ApplicationModel.Permissions
    /// </summary>
    public class PermissionService : IPermissionService
    {
        private readonly IDialogService _dialogService;
        public PermissionService(IDialogService dialogService) 
        { 
            _dialogService = dialogService;
        }
        public async Task<PermissionStatus> CheckAndRequestLocationPermission()
        {
            PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            if (status == PermissionStatus.Granted)
                return status;

            if (status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS)
            {
                return status;
            }

            if (Permissions.ShouldShowRationale<Permissions.LocationWhenInUse>())
            {
                await _dialogService.ShowAlert("BLE Example needs location as part of ble scanning", "", "Ok");
            }

            status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

            return status;
        }

        public async Task<PermissionStatus> CheckAndRequestBLEPermission()
        {
            
            PermissionStatus status = await Permissions.CheckStatusAsync<BLEPermission>();

            if (status == PermissionStatus.Granted)
                return status;

            status = await Permissions.RequestAsync<BLEPermission>();

            return status;

        }
    }
}
