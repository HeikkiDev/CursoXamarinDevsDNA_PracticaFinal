using AppCursoXamarinDevsDNA.Services.Analytics;
using AppCursoXamarinDevsDNA.Services.AppProperties;
using AppCursoXamarinDevsDNA.Services.NavigationService;
using ReactiveUI;
using Splat;

namespace AppCursoXamarinDevsDNA.Base
{
    public abstract class BaseViewModel : ReactiveObject
    {
        private readonly INavigationService _navigationService;
        private readonly IAnalyticsService _analyticsService;
        private readonly IAppPropertiesService _appPropertiesService;

        public INavigationService NavigationService
        {
            get => _navigationService;
        }

        public IAnalyticsService AnalyticsService
        {
            get => _analyticsService;
        }

        public IAppPropertiesService AppPropertiesService
        {
            get => _appPropertiesService;
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { this.RaiseAndSetIfChanged(ref _isBusy, value); }
        }

        public BaseViewModel(INavigationService navigationService = null, IAnalyticsService analyticsService = null, IAppPropertiesService appPropertiesService = null)
        {
            _navigationService = navigationService ?? Locator.Current.GetService<INavigationService>();
            _analyticsService = analyticsService ?? Locator.Current.GetService<IAnalyticsService>();
            _appPropertiesService = appPropertiesService ?? Locator.Current.GetService<IAppPropertiesService>();
        }

        public virtual void Load(NavigationParameters navigationParameters)
        {

        }
    }
}
