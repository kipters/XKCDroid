using Android;
using Android.App;
using Android.Content.PM;
using Android.Support.V4.App;
using Android.Support.V4.Content;

namespace XKCDroid.Utils
{
    public class MarshmallowPermissions
    {
        private const int RequestExternalStorage = 1;

        private static readonly string[] PermissionsStorage =
        {
            Manifest.Permission.ReadExternalStorage,
            Manifest.Permission.WriteExternalStorage
        };

        public static bool RequestWriteStorage(Activity activity)
        {
            var permission = ContextCompat.CheckSelfPermission(activity, Manifest.Permission.WriteExternalStorage);
            if (permission != Permission.Granted)
                ActivityCompat.RequestPermissions(activity, PermissionsStorage, RequestExternalStorage);

            return ContextCompat.CheckSelfPermission(activity, Manifest.Permission.WriteExternalStorage) == Permission.Granted;
        }
    }
}