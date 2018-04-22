using System.Threading.Tasks;

namespace AppCursoXamarinDevsDNA.Features.Login
{
    public interface ILoginService
    {
        Task<bool> LoginWithUserAndPassword(string user, string password);
    }
}