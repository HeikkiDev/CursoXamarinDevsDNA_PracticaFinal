using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AppCursoXamarinDevsDNA.Services.PlaceAutocomplete;
using Foundation;
using MapKit;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(AppCursoXamarinDevsDNA.iOS.Services.PlaceAutocompleteService))]
namespace AppCursoXamarinDevsDNA.iOS.Services
{
    public class PlaceAutocompleteService : IPlaceAutocompleteService
    {
        public static List<PlaceDetailsResponse> _listPlacesDetails;

        public PlaceAutocompleteService()
        {
            _listPlacesDetails = new List<PlaceDetailsResponse>();
        }

        public async Task<List<AutocompletePlace>> GetPlaces(string textToSearch, CancellationToken cancellationToken, string components = null)
        {
            List<AutocompletePlace> autocompletePlaces = new List<AutocompletePlace>();

            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var response = await Search(textToSearch, cancellationToken);
                if (response == null) return autocompletePlaces;

                _listPlacesDetails.Clear();
                foreach (var item in response)
                {
                    string uniqueId = ToHashedBase64("Id_" + item.GetHashCode());

                    autocompletePlaces.Add(
                            new AutocompletePlace()
                            {
                                ID = uniqueId,
                                Place_ID = uniqueId,
                                Description = item.Placemark.Title,
                                Reference = uniqueId
                            }
                        );

                    _listPlacesDetails.Add(
                            new PlaceDetailsResponse()
                            {
                                ID = uniqueId,
                                Place_ID = uniqueId,
                                result = new PlaceDetailsResponse.Result()
                                {
                                    geometry = new PlaceDetailsResponse.Geometry()
                                    {
                                        location = new PlaceDetailsResponse.Location()
                                        {
                                            lat = item.Placemark.Coordinate.Latitude,
                                            lng = item.Placemark.Coordinate.Longitude
                                        }
                                    }
                                }
                            }
                        );
                }

                cancellationToken.ThrowIfCancellationRequested();
            }
            catch (Exception ex)
            {
                string algo = "";
            }

            return autocompletePlaces;
        }

        public async Task<PlaceDetailsResponse> GetPlaceDetails(string placeId, CancellationToken cancellationToken)
        {
            var resultPlace = from place in _listPlacesDetails
                              where place.Place_ID == placeId
                              select place;

            cancellationToken.ThrowIfCancellationRequested();

            return await Task.FromResult(resultPlace.First());
        }

        private async Task<List<MKMapItem>> Search(string forSearchString, CancellationToken cancellationToken)
        {

            MKLocalSearchRequest request = new MKLocalSearchRequest();
            request.NaturalLanguageQuery = forSearchString;
            MKLocalSearch search = new MKLocalSearch(request);

            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                MKLocalSearchResponse response = await search.StartAsync();

                cancellationToken.ThrowIfCancellationRequested();

                return response.MapItems.ToList();
            }
            catch
            {
                return null;
            }
        }

        public static string ToHashedBase64(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            using (var sha256 = new SHA256Managed())
            {
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}