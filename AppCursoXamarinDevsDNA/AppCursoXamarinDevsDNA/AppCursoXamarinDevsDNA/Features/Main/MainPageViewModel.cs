using AppCursoXamarinDevsDNA.Base;
using AppCursoXamarinDevsDNA.Services.NavigationService;
using ReactiveUI;
using System.Threading.Tasks;

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
            NavigationParameters navigationParameters = new NavigationParameters();
            navigationParameters.Add("detail-text", "Texto asignado desde Load!!");

            var page = new DetailPage.DetailPage(navigationParameters);
            await NavigationService.PushTo(page);
        }
    }
}
