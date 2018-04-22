using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppCursoXamarinDevsDNA.Services.NavigationService
{
    public interface INavigationService
    {
        void SetMainPage(Page page);
        Task<Page> Back();
        Task<Page> BackModal(bool animated = true);
        Task BackToRoot();
        Task PushModalTo(Page page, bool animated = true);
        Task PushTo(Page page);
    }
}