using Newtonsoft.Json;
using System.Collections.Generic;

namespace AppCursoXamarinDevsDNA.Services.PlaceAutocomplete
{
    /// <summary>
    ///  Google Places - Place Autocomplete response
    /// https://developers.google.com/places/web-service/autocomplete
    /// </summary>
    public class AutocompleteResponse
    {
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the auto complete places.
        /// </summary>
        /// <value>The auto complete places.</value>
        [JsonProperty("predictions")]
        public List<AutocompletePlace> AutoCompletePlaces { get; set; }
    }
}
