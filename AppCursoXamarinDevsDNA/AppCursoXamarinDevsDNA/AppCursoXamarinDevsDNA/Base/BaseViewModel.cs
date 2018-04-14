using AppCursoXamarinDevsDNA.Services.NavigationService;
using ReactiveUI;
using Splat;

namespace AppCursoXamarinDevsDNA.Base
{
    public abstract class BaseViewModel : ReactiveObject
    {
        private readonly INavigationService _navigationService;
        public INavigationService NavigationService
        {
            get => _navigationService;
        }

        public BaseViewModel(INavigationService navigationService = null)
        {
            _navigationService = navigationService ?? Locator.Current.GetService<INavigationService>();
        }
    }
}
