using System;
using System.Collections.Generic;
using System.Text;

namespace AppCursoXamarinDevsDNA.Services.PlaceAutocomplete
{
    public class PlaceDetailsResponse
    {
        // For iOS search details
        public string ID { get; set; }
        public string Place_ID { get; set; }

        public Result result { get; set; }
        public string status { get; set; }


        public class Result
        {
            public PlaceDetailsResponse.Geometry geometry { get; set; }
        }

        public class Geometry
        {
            public Location location { get; set; }
        }

        public class Location
        {
            public double lat { get; set; }
            public double lng { get; set; }
        }
    }
}
