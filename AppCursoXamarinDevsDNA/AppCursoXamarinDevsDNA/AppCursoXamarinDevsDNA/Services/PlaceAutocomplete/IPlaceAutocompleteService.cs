using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppCursoXamarinDevsDNA.Services.PlaceAutocomplete
{
    public interface IPlaceAutocompleteService
    {
        Task<List<AutocompletePlace>> GetPlaces(string textToSearch, CancellationToken cancellationToken, string components = null);
        Task<PlaceDetailsResponse> GetPlaceDetails(string textToSplaceIdearch, CancellationToken cancellationToken);
    }
}
