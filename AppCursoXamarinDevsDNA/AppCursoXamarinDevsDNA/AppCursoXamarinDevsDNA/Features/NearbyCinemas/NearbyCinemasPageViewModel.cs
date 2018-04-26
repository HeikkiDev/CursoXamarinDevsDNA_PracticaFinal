using AppCursoXamarinDevsDNA.Base;
using AppCursoXamarinDevsDNA.Services.Analytics;
using AppCursoXamarinDevsDNA.Services.AppProperties;
using AppCursoXamarinDevsDNA.Services.NavigationService;
using AppCursoXamarinDevsDNA.Services.NearbyCinemas;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace AppCursoXamarinDevsDNA.Features.NearbyCinemas
{
    public static class NavigationKeys
    {
        public const string MAP_POSITION = "map-position";
    }

    public class NearbyCinemasPageViewModel : BaseViewModel
    {
        private readonly INearbyCinemasService _nearbyCinemasService;

        #region BINDABLE PROPERTIES
        private Cinema _selectedItem;
        public Cinema SelectedItem
        {
            get { return _selectedItem; }
            set { this.RaiseAndSetIfChanged(ref _selectedItem, value); }
        }

        private List<Cinema> _cinemasList;
        public List<Cinema> CinemasList
        {
            get { return _cinemasList; }
            set { this.RaiseAndSetIfChanged(ref _cinemasList, value); }
        }
        #endregion

        public NearbyCinemasPageViewModel() : this(null,null,null,null)
        {

        }

        public NearbyCinemasPageViewModel(INavigationService navigationService, IAnalyticsService analyticsService, IAppPropertiesService appPropertiesService, INearbyCinemasService nearbyCinemasService)
            : base(navigationService, analyticsService, appPropertiesService)
        {
            _nearbyCinemasService = nearbyCinemasService ?? Locator.Current.GetService<INearbyCinemasService>();
        }

        public override async void Load(NavigationParameters navigationParameters)
        {
            base.Load(navigationParameters);

            try
            {
                if (navigationParameters.ContainsKey(NavigationKeys.MAP_POSITION))
                {
                    Position mapPosition = (Position)navigationParameters[NavigationKeys.MAP_POSITION];

                    CinemasList = await _nearbyCinemasService.GetCinemas(mapPosition);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error en Load() en NearbyCinemasPageViewModel:");
                Debug.WriteLine(ex.Message);
            }
        }

        #region COMMANDS
        public ICommand ItemTappedCommand => (new Command(
          (object sender) =>
          {
              SelectedItem = null;
          }));
        #endregion
    }
}
