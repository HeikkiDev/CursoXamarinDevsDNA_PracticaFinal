using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppCursoXamarinDevsDNA.Services.NavigationService
{
    public class NavigationService : INavigationService
    {
        public NavigationService()
        {

        }

        public async Task PushTo(Page page)
        {
            await Application.Current.MainPage.Navigation.PushAsync(page);
        }

        public async Task PushModalTo(Page page, bool animated)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(page, animated);
        }

        public async Task Back()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        public async Task BackModal(bool animated)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync(animated);
        }

        public async Task BackToRoot()
        {
            await Application.Current.MainPage.Navigation.PopToRootAsync();
        }

    }
}
