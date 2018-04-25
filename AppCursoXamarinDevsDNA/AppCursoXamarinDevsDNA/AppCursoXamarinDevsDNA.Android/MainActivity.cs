using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using AppCursoXamarinDevsDNA.Services.Gps;

namespace AppCursoXamarinDevsDNA.Droid
{
    [Activity(Label = "AppCursoXamarinDevsDNA", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private const int LOCATION_PERMISSION_ID = 2;
        private IGpsService gpsService;

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());

            Xamarin.FormsMaps.Init(this, bundle);

            gpsService = Xamarin.Forms.DependencyService.Get<IGpsService>();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            switch (requestCode)
            {
                case LOCATION_PERMISSION_ID:
                    {
                        if (grantResults[0] == Permission.Granted)
                        {
                            //Permission granted
                            gpsService.OnRequestPermissionsResult(true);
                        }
                        else
                        {
                            //Permission Denied :(
                            gpsService.OnRequestPermissionsResult(false);
                        }
                    }
                    break;
            }
        }
    }
}

