using System;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Locations;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using AppCursoXamarinDevsDNA.Services.Gps;
using Plugin.CurrentActivity;

[assembly: Xamarin.Forms.Dependency(typeof(AppCursoXamarinDevsDNA.Droid.Services.GpsService))]
namespace AppCursoXamarinDevsDNA.Droid.Services
{
    public class GpsService : Java.Lang.Object, IGpsService
    {
        private const int LOCATION_PERMISSION_ID = 2;
        private string[] permissions = { Manifest.Permission.AccessCoarseLocation, Manifest.Permission.AccessFineLocation };
        private LocationManager locationManager;

        private Context CurrentContext => CrossCurrentActivity.Current.Activity;
        private TaskCompletionSource<Coordinates> tcs;
        private TaskCompletionSource<bool> tcsPermissions;

        public Task<bool> GetPermissionsAsync()
        {
            tcsPermissions = new TaskCompletionSource<bool>();

            if ((int)Build.VERSION.SdkInt < 23) // Permissions only for Marshmallow and up
            {
                tcsPermissions.TrySetResult(true);
            }
            else
            {
                if (ActivityCompat.CheckSelfPermission(CurrentContext, Manifest.Permission.AccessCoarseLocation) != (int)Android.Content.PM.Permission.Granted)
            {
                RequestPhonePermissions();
            }
            else
                tcsPermissions.TrySetResult(true);
            }

            return tcsPermissions.Task;
        }

        public Task<Coordinates> GetCurrentPositionAsync()
        {
            tcs = new TaskCompletionSource<Coordinates>();
            Coordinates currentPosition = null;

            Permission locationPermission = ContextCompat.CheckSelfPermission(CurrentContext, Manifest.Permission.AccessCoarseLocation);
            if (locationPermission == Permission.Granted)
            {
                locationManager = CurrentContext.GetSystemService(Context.LocationService) as LocationManager;
                string provider = GetBestLocationProvider();
                var result = locationManager.GetLastKnownLocation(provider);
                if (result != null)
                {
                    currentPosition = new Coordinates()
                    {
                        Latitude = result.Latitude,
                        Longitude = result.Longitude
                    };

                    tcs.SetResult(currentPosition);
                }
                else
                    tcs.SetResult(default(Coordinates));
            }
            else
                tcs.SetResult(default(Coordinates));

            return tcs.Task;
        }

        public void OnRequestPermissionsResult(bool isGranted)
        {
            if (isGranted)
            {
                //Permission granted
                tcsPermissions.TrySetResult(true);
            }
            else
            {
                //Permission Denied :(
                tcsPermissions.TrySetResult(false);
            }
        }

        private void RequestPhonePermissions()
        {
            var currentActivity = (Activity)CurrentContext;
            if (ActivityCompat.ShouldShowRequestPermissionRationale(currentActivity, Manifest.Permission.AccessCoarseLocation))
            {
                Snackbar.Make(currentActivity.FindViewById((Android.Resource.Id.Content)), "App need location to use with maps.", Snackbar.LengthIndefinite).SetAction("Ok", v =>
                {
                    ((Activity)CurrentContext).RequestPermissions(permissions, LOCATION_PERMISSION_ID);
                }).Show();
            }
            else
            {
                ActivityCompat.RequestPermissions(((Activity)CurrentContext), permissions, LOCATION_PERMISSION_ID);
            }
        }

        private string GetBestLocationProvider()
        {
            Criteria providerSearchCriteria = new Criteria() { Accuracy = Accuracy.Coarse };
            return locationManager.GetBestProvider(providerSearchCriteria, false);
        }
    }
}