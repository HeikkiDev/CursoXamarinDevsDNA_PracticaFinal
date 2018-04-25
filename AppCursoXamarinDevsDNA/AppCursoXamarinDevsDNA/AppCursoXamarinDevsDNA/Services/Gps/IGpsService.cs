using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppCursoXamarinDevsDNA.Services.Gps
{
    public interface IGpsService
    {
        Task<bool> GetPermissionsAsync();
        Task<Coordinates> GetCurrentPositionAsync();
        void OnRequestPermissionsResult(bool isGranted);
    }
}
