using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppCursoXamarinDevsDNA.Services.NavigationService
{
    public interface INavigationService
    {
        void SetMainPage(Page page);
        Task Back();
        Task BackModal(bool animated);
        Task BackToRoot();
        Task PushModalTo(Page page, bool animated);
        Task PushTo(Page page);
    }
}