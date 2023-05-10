
namespace BLEExample.Services.PermissionRequests
{
    /// <summary>
    /// Definition of the permission service contract
    /// </summary>
    public interface IPermissionService
    {
        public Task<PermissionStatus> CheckAndRequestLocationPermission();
        public Task<PermissionStatus> CheckAndRequestBLEPermission();
    }
}
