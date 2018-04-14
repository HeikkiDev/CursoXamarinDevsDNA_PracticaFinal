using AppCursoXamarinDevsDNA.Base;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppCursoXamarinDevsDNA.Features.Main
{
    public class MainPageViewModel : BaseViewModel
    {
        private ReactiveCommand _navigateToDetailCommand;
        public ReactiveCommand NavigateToDetailCommand => _navigateToDetailCommand;

        public MainPageViewModel()
        {
            _navigateToDetailCommand = ReactiveCommand.CreateFromTask(NavigateToDetailAsync);
        }

        private async Task NavigateToDetailAsync()
        {
            //var page = new DetailPage.DetailPage();
            var page = (ContentPage)Splat.Locator.Current.GetService(typeof(DetailPage.DetailPageViewModel));
            await NavigationService.PushTo(page);
        }
    }
}
