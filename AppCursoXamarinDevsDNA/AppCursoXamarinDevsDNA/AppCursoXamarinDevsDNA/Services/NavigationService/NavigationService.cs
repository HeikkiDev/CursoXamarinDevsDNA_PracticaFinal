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

        public void SetMainPage(Page page)
        {
            Application.Current.MainPage = new NavigationPage(page);
        }

        public async Task PushTo(Page page)
        {
            await Application.Current.MainPage.Navigation.PushAsync(page);
        }

        public async Task PushModalTo(Page page, bool animated = true)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(page, animated);
        }

        public async Task<Page> Back()
        {
            return await Application.Current.MainPage.Navigation.PopAsync();
        }

        public async Task<Page> BackModal(bool animated = true)
        {
            return await Application.Current.MainPage.Navigation.PopModalAsync(animated);
        }

        public async Task BackToRoot()
        {
            await Application.Current.MainPage.Navigation.PopToRootAsync();
        }

    }
}
