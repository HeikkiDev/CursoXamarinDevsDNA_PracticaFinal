using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppCursoXamarinDevsDNA.Services.NavigationService
{
    public interface INavigationService
    {
        Task Back();
        Task BackModal(bool animated);
        Task BackToRoot();
        Task PushModalTo(Page page, bool animated);
        Task PushTo(Page page);
    }
}