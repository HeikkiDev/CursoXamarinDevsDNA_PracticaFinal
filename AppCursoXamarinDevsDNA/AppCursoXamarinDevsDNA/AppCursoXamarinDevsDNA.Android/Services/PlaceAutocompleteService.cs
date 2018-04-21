using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Android.Content;
using AppCursoXamarinDevsDNA.Services.PlaceAutocomplete;
using Newtonsoft.Json;
using Plugin.CurrentActivity;

[assembly: Xamarin.Forms.Dependency(typeof(AppCursoXamarinDevsDNA.Droid.Services.PlaceAutocompleteService))]
namespace AppCursoXamarinDevsDNA.Droid.Services
{
    /// <summary>
    /// Servicio para usar la Google Places API en Android como dependencia.
    /// </summary>
    public class PlaceAutocompleteService : IPlaceAutocompleteService
    {
        private Context CurrentContext => CrossCurrentActivity.Current.Activity;
        private static string GOOGLE_PLACE_AUTOCOMPLETE_URL;
        private static string GOOGLE_PLACE_DETAIL_URL;
        private static string GOOGLE_PLACE_API;

        public PlaceAutocompleteService()
        {
            var appInfo = CurrentContext.PackageManager.GetApplicationInfo(CurrentContext.PackageName, Android.Content.PM.PackageInfoFlags.MetaData);
            GOOGLE_PLACE_AUTOCOMPLETE_URL = appInfo.MetaData.GetString("com.curso.devsdna.GOOGLE_PLACE_AUTOCOMPLETE_URL");
            GOOGLE_PLACE_DETAIL_URL = appInfo.MetaData.GetString("com.curso.devsdna.GOOGLE_PLACE_DETAIL_URL");
            GOOGLE_PLACE_API = appInfo.MetaData.GetString("com.google.android.geo.API_KEY");
        }

        public async Task<List<AutocompletePlace>> GetPlaces(string textToSearch, CancellationToken cancellationToken, string components = null)
        {
            List<AutocompletePlace> placesList = new List<AutocompletePlace>();

            if (string.IsNullOrEmpty(GOOGLE_PLACE_API))
            {
                throw new Exception("You have not assigned Google Geo Api Key in Android Manifest.");
            }

            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                var requestURI = CreatePredictionsUri(textToSearch);
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, requestURI);
                var response = await client.SendAsync(request);

                cancellationToken.ThrowIfCancellationRequested();

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var result = await response.Content.ReadAsStringAsync();

                cancellationToken.ThrowIfCancellationRequested();

                var autocompleteResponse = JsonConvert.DeserializeObject<AutocompleteResponse>(result);
                placesList = autocompleteResponse.AutoCompletePlaces;

                cancellationToken.ThrowIfCancellationRequested();

                return placesList;
            }
            catch (Exception ex)
            {
                cancellationToken.ThrowIfCancellationRequested();
                System.Diagnostics.Debug.WriteLine("Google Places http request throw expcepton: " + ex.Message);
                return placesList;
            }
            finally
            {
                cancellationToken.ThrowIfCancellationRequested();
            }
        }

        public async Task<PlaceDetailsResponse> GetPlaceDetails(string placeId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(GOOGLE_PLACE_API))
            {
                throw new Exception("You have not assigned Google Geo Api Key in Android Manifest.");
            }

            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                var requestURI = CreateDetailsUri(placeId);
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, requestURI);
                var response = await client.SendAsync(request);

                cancellationToken.ThrowIfCancellationRequested();

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var result = await response.Content.ReadAsStringAsync();

                cancellationToken.ThrowIfCancellationRequested();

                var placeDetailsResponse = JsonConvert.DeserializeObject<PlaceDetailsResponse>(result);

                cancellationToken.ThrowIfCancellationRequested();

                return placeDetailsResponse;
            }
            catch (Exception ex)
            {
                cancellationToken.ThrowIfCancellationRequested();
                System.Diagnostics.Debug.WriteLine("Google Places Details http request throw expcepton: " + ex.Message);
                return null;
            }
            finally
            {
                cancellationToken.ThrowIfCancellationRequested();
            }
        }

        /// <summary>
        /// Creates the predictions URI.
        /// </summary>
        /// <returns>The predictions URI.</returns>
        /// <param name="newTextValue">New text value.</param>
        /// <param name="components">Components param value.</param>
        string CreatePredictionsUri(string textToSearch, string components = null)
        {
            var input = Uri.EscapeUriString(textToSearch);
            var constructedUrl = $"{GOOGLE_PLACE_AUTOCOMPLETE_URL}?input={input}&key={GOOGLE_PLACE_API}";

            // Example component: "country:es" -> Restrict results to Spain
            // Example components: "country:au|country:nz" -> Restrict results to Australia and New Zealand
            if (components != null)
                constructedUrl += $"&components={components}";

            return constructedUrl;
        }

        /// <summary>
        /// Creates the Place Details URI.
        /// </summary>
        /// <returns>The Place Details URI.</returns>
        /// <param name="newTextValue">Place Id value to search.</param>
        string CreateDetailsUri(string placeId)
        {
            var place_id = Uri.EscapeUriString(placeId);
            var constructedUrl = $"{GOOGLE_PLACE_DETAIL_URL}?placeid={place_id}&key={GOOGLE_PLACE_API}";

            return constructedUrl;
        }
    }
}