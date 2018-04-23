using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace AppCursoXamarinDevsDNA.Services.NearbyCinemas
{
    public class NearbyCinemasService : INearbyCinemasService
    {
        public async Task<List<Cinema>> GetCinemas(Position centerPoint)
        {
            await Task.Delay(1000); // para simular una request http

            return Cinema.GetFakeCinemas();
        }
    }
}
