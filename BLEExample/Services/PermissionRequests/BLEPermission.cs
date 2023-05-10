using static Microsoft.Maui.ApplicationModel.Permissions;

namespace BLEExample.Services.PermissionRequests
{
    /// <summary>
    /// Handle BLE permissions which are not part of the Maui Essentials Permissions feature set as of this time
    /// </summary>
    internal class BLEPermission : BasePlatformPermission
    {
#if ANDROID
        public override(string androidPermission, bool isRuntime)[] RequiredPermissions => 
              new List<(string permission, bool isRuntime)>
              {
                   ("android.permission.BLUETOOTH_SCAN", true),
                   ("android.permission.BLUETOOTH_CONNECT", true)
              }.ToArray();
#endif

#if IOS
        //Not implemented
#endif

    }
}
