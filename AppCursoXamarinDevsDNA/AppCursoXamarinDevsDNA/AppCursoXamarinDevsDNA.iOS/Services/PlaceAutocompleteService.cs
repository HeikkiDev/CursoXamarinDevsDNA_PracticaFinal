using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AppCursoXamarinDevsDNA.Services.PlaceAutocomplete;
using Foundation;
using UIKit;

namespace AppCursoXamarinDevsDNA.iOS.Services
{
    public class PlaceAutocompleteService : IPlaceAutocompleteService
    {
        public PlaceAutocompleteService()
        {

        }

        public async Task<List<AutocompletePlace>> GetPlaces(string textToSearch, CancellationToken cancellationToken, string components = null)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return new List<AutocompletePlace>();
        }

        public async Task<PlaceDetailsResponse> GetPlaceDetails(string placeId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return new PlaceDetailsResponse();
        }
    }
}