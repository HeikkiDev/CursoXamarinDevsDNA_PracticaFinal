using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace AppCursoXamarinDevsDNA.Services.NearbyCinemas
{
    public interface INearbyCinemasService
    {
        Task<List<Cinema>> GetCinemas(Position centerPoint);
    }
}