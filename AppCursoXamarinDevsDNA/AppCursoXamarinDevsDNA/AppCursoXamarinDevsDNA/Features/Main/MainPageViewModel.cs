using AppCursoXamarinDevsDNA.Base;
using AppCursoXamarinDevsDNA.CustomControls;
using AppCursoXamarinDevsDNA.Services.Analytics;
using AppCursoXamarinDevsDNA.Services.NavigationService;
using AppCursoXamarinDevsDNA.Services.PlaceAutocomplete;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace AppCursoXamarinDevsDNA.Features.Main
{
    public class MainPageViewModel : BaseViewModel
    {
        //private ReactiveCommand _toolbarItemCommand;
        //public ReactiveCommand ToolbarItemCommand => _toolbarItemCommand;

        private ReactiveCommand _navigateToPlaceSearchbarCommand;
        public ReactiveCommand NavigateToPlaceSearchbarCommand => _navigateToPlaceSearchbarCommand;
        
        private ReactiveCommand _navigateToMoviesCommand;
        public ReactiveCommand NavigateToMoviesCommand => _navigateToMoviesCommand;

        private ReactiveCommand _mapTappedCommand;
        public ReactiveCommand MapTappedCommand => _mapTappedCommand;

        private ReactiveCommand _closeMapSelectionCommand;
        public ReactiveCommand CloseMapSelectionCommand => _closeMapSelectionCommand;

        private ReactiveCommand _okMapSelectionCommand;
        public ReactiveCommand OkMapSelectionCommand => _okMapSelectionCommand;

        #region BINDABLE PROPERTIES
        private SelectPlaceMap.SelectionType _selectionType;
        public SelectPlaceMap.SelectionType SelectionType
        {
            get { return _selectionType; }
            set { this.RaiseAndSetIfChanged(ref _selectionType, value); }
        }

        private bool _isMapSelectionEnable;
        public bool IsMapSelectionEnable
        {
            get { return _isMapSelectionEnable; }
            set { this.RaiseAndSetIfChanged(ref _isMapSelectionEnable, value); }
        }

        private bool _isVisibleShowMoviesButton;
        public bool IsVisibleShowMoviesButton
        {
            get { return _isVisibleShowMoviesButton; }
            set { this.RaiseAndSetIfChanged(ref _isVisibleShowMoviesButton, value); }
        }

        private string _textSearched;
        public string TextSearched
        {
            get { return _textSearched; }
            set { this.RaiseAndSetIfChanged(ref _textSearched, value); }
        }

        private Position _centerPosition;
        public Position CenterPosition
        {
            get { return _centerPosition; }
            set { this.RaiseAndSetIfChanged(ref _centerPosition, value); }
        }

        private AutocompletePlace _currentPinPlace;
        public AutocompletePlace CurrentPinPlace
        {
            get { return _currentPinPlace; }
            set { this.RaiseAndSetIfChanged(ref _currentPinPlace, value); }
        }

        private PlaceDetailsResponse _currentPinPlaceDetails;
        public PlaceDetailsResponse CurrentPinPlaceDetails
        {
            get { return _currentPinPlaceDetails; }
            set { this.RaiseAndSetIfChanged(ref _currentPinPlaceDetails, value); }
        }
        #endregion

        public MainPageViewModel()
        {
            //_toolbarItemCommand = ReactiveCommand.Create(LogoutToolbarCommand);
            _navigateToPlaceSearchbarCommand = ReactiveCommand.CreateFromTask(NavigateToPlaceSearchbarAsync);
            _navigateToMoviesCommand = ReactiveCommand.CreateFromTask(NavigateToMoviesAsync);
            _mapTappedCommand = ReactiveCommand.Create<bool>(param => MapTappedAsync(param));
            _closeMapSelectionCommand = ReactiveCommand.Create(CloseMapSelectionModeAsync);
            _okMapSelectionCommand = ReactiveCommand.Create(OkMapSelectionAsync);
        }

        public override void Load(NavigationParameters navigationParameters)
        {
            base.Load(navigationParameters);

            try
            {
                IsMapSelectionEnable = false;
                TextSearched = "Busque aquí";

                //CenterPosition = await _geoLocationService.GetDeviceCoordinates();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error en Load() en MainPageViewModel:");
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        #region COMMAND HANDLERS
        public ICommand LogoutToolbarItemCommand => (new Command(
          () =>
          {
              if (AppPropertiesService.ContainsKey("user_login_token"))
              {
                  try
                  {
                      string userId = AppPropertiesService.Get("user_login_token");

                      AnalyticsService.TrackEvent(AppCenterEvents.USER_LOG_OUT, new Dictionary<string, string> {
                        { "userId", userId }
                  });
                  }
                  catch { }

                  AppPropertiesService.Remove("user_login_token");
              }

              NavigationService.SetMainPage(new Login.LoginPage());
          }));
        //private void LogoutToolbarCommand()
        //{
        //    string userId = AppPropertiesService.Get("user_login_token");
        //    AppPropertiesService.Remove("user_login_token");

        //    AnalyticsService.TrackEvent(AppCenterEvents.USER_LOG_OUT, new Dictionary<string, string> {
        //                { "userId", userId }
        //          });

        //    NavigationService.SetMainPage(new Login.LoginPage());
        //}

        private async Task NavigateToPlaceSearchbarAsync()
        {
            var page = new PlaceSearch.PlaceSearchPage();
            page.OnBackNavigation += PlaceSearchPage_OnBackNavigation;
            await NavigationService.PushModalTo(page);
        }

        private void PlaceSearchPage_OnBackNavigation(object sender, NavigationParameters navigationParameters)
        {
            PlaceSearch.PlaceSearchPage page = (PlaceSearch.PlaceSearchPage)sender;
            page.OnBackNavigation -= PlaceSearchPage_OnBackNavigation;

            NavigationService.BackModal();

            if (navigationParameters != null && navigationParameters.ContainsKey("place-selected"))
            {
                AutocompletePlace selectedPlace = (AutocompletePlace)navigationParameters["place-selected"];

                if (selectedPlace.ID == "select_in_map")
                {
                    SelectionType = CustomControls.SelectPlaceMap.SelectionType.IN_MAP;
                    IsVisibleShowMoviesButton = false;
                    IsMapSelectionEnable = true;
                    TextSearched = "Busque aquí"; // Reset search text
                }
                else
                {
                    TextSearched = selectedPlace.Description;
                    IsVisibleShowMoviesButton = true;
                }

                CurrentPinPlace = selectedPlace;
            }
        }

        private async Task NavigateToMoviesAsync()
        {
            NavigationParameters navigationParameters = new NavigationParameters();
            navigationParameters.Add(DetailPage.NavigationKeys.DETAIL_TEXT, "Texto asignado desde Load!!");

            var page = new DetailPage.DetailPage(navigationParameters);
            await NavigationService.PushTo(page);
        }

        private void MapTappedAsync(bool isMapSelectionEnalbeOnTap)
        {
            if ((bool)isMapSelectionEnalbeOnTap)
            {
                IsMapSelectionEnable = false;
                IsVisibleShowMoviesButton = true;
                TextSearched = "Busque aquí"; // Reset search text
            }
            else
            {
                IsVisibleShowMoviesButton = false;
                CurrentPinPlace = null; // Remove Pin
                TextSearched = "Busque aquí"; // Reset search text
            }

            if (SelectionType == SelectPlaceMap.SelectionType.IN_MAP)
            {
                // Reset selection type
                SelectionType = SelectPlaceMap.SelectionType.SEARCHBAR;
            }
        }

        private void CloseMapSelectionModeAsync()
        {
            SelectionType = SelectPlaceMap.SelectionType.SEARCHBAR;
            IsMapSelectionEnable = false;
            CurrentPinPlace = null;
        }

        private void OkMapSelectionAsync()
        {
            SelectionType = SelectPlaceMap.SelectionType.SEARCHBAR;
            IsMapSelectionEnable = false;
            IsVisibleShowMoviesButton = true;
            TextSearched = "Busque aquí"; // Reset search text
        }
        #endregion
    }
}
