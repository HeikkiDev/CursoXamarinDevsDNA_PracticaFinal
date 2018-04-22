using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppCursoXamarinDevsDNA.Features.Login
{
    // Servicio para Login fake
    public class LoginService : ILoginService
    {
        public async Task<bool> LoginWithUserAndPassword(string user, string password)
        {
            await Task.Delay(1000); // para simular una request http

            if (user == "enrique" && password == "password1")
                return true;
            else
                return false;
        }
    }
}
