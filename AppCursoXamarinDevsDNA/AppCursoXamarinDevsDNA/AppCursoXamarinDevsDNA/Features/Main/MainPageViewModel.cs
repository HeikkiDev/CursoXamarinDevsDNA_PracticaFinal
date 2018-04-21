using AppCursoXamarinDevsDNA.Base;
using AppCursoXamarinDevsDNA.CustomControls;
using AppCursoXamarinDevsDNA.Services.NavigationService;
using AppCursoXamarinDevsDNA.Services.PlaceAutocomplete;
using ReactiveUI;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace AppCursoXamarinDevsDNA.Features.Main
{
    public class MainPageViewModel : BaseViewModel
    {
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
            _navigateToPlaceSearchbarCommand = ReactiveCommand.CreateFromTask(NavigateToPlaceSearchbarAsync);
            _navigateToMoviesCommand = ReactiveCommand.CreateFromTask(NavigateToMoviesAsync);
            _mapTappedCommand = ReactiveCommand.Create<bool>(param => MapTappedAsync(param));
            _closeMapSelectionCommand = ReactiveCommand.Create(CloseMapSelectionModeAsync);
            _okMapSelectionCommand = ReactiveCommand.Create(OkMapSelectionAsync);
        }

        #region COMMAND HANDLERS
        private async Task NavigateToPlaceSearchbarAsync()
        {
            //TODO
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
