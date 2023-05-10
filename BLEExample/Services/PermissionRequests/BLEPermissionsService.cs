
namespace BLEExample.Services.PermissionRequests
{
    /// <summary>
    /// Implmenetation of the partial class approach to platform code
    /// For checking BLE permissions which are not part of the Essentials package
    /// </summary>
    public partial class BLEPermissionsService : Permissions.BasePlatformPermission
    {
     
        private async Task<bool> CheckBluetoothStatus()
        {
            try
            {
                var requestStatus = await new BLEPermissionsService().CheckStatusAsync();
                return requestStatus == PermissionStatus.Granted;
            }
            catch (Exception ex)
            {
                //TODO
                return false;
            }
        }

        public async Task<bool> RequestBluetoothAccess()
        {
            try
            {
                var requestStatus = await new BLEPermissionsService().RequestAsync();
                return requestStatus == PermissionStatus.Granted;
            }
            catch (Exception ex)
            {
               //TODO
                return false;
            }
        }
    }
}
