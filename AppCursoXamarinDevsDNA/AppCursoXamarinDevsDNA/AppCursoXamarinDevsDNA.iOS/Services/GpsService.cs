using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCursoXamarinDevsDNA.Services.Gps;
using CoreLocation;
using Foundation;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(AppCursoXamarinDevsDNA.iOS.Services.GpsService))]
namespace AppCursoXamarinDevsDNA.iOS.Services
{
    public class GpsService : CLLocationManagerDelegate, IGpsService
    {
        private CLLocationManager locationManager;
        private TaskCompletionSource<Coordinates> tcs;
        private TaskCompletionSource<bool> tcsPermissions;

        public Task<bool> GetPermissionsAsync()
        {
            tcsPermissions = new TaskCompletionSource<bool>();
            locationManager = new CLLocationManager();
            locationManager.Delegate = this;
            locationManager.RequestWhenInUseAuthorization();

            return tcsPermissions.Task;
        }

        public Task<Coordinates> GetCurrentPositionAsync()
        {
            tcs = new TaskCompletionSource<Coordinates>();

            locationManager = new CLLocationManager();
            locationManager.Delegate = this;
            locationManager.RequestWhenInUseAuthorization();
            if (CLLocationManager.Status == CLAuthorizationStatus.Authorized
                || CLLocationManager.Status == CLAuthorizationStatus.AuthorizedAlways
                || CLLocationManager.Status == CLAuthorizationStatus.AuthorizedWhenInUse)
            {
                locationManager.RequestLocation();
            }
            else
            {
                tcs.TrySetResult(default(Coordinates));
            }

            return tcs.Task;
        }

        public override void AuthorizationChanged(CLLocationManager manager, CLAuthorizationStatus status)
        {
            if(status == CLAuthorizationStatus.Authorized 
                || status == CLAuthorizationStatus.AuthorizedAlways 
                || status == CLAuthorizationStatus.AuthorizedWhenInUse)
            {
                tcsPermissions.TrySetResult(true);
            }
            else if(status == CLAuthorizationStatus.Denied || status == CLAuthorizationStatus.Restricted)
            {
                tcsPermissions.TrySetResult(false);
            }
        }

        public override void UpdatedLocation(CLLocationManager manager, CLLocation newLocation, CLLocation oldLocation) { }

        public override void Failed(CLLocationManager manager, Foundation.NSError error) { }

        public override void LocationsUpdated(CLLocationManager manager, CLLocation[] locations)
        {
            if (locations != null && locations.Any())
                SetCurrentLocation(locations.First());
        }

        private void SetCurrentLocation(CLLocation currentLocation)
        {
            Coordinates currentPosition = new Coordinates();

            currentPosition.Latitude = currentLocation.Coordinate.Latitude;
            currentPosition.Longitude = currentLocation.Coordinate.Longitude;

            tcs.TrySetResult(currentPosition);
        }
    }
}