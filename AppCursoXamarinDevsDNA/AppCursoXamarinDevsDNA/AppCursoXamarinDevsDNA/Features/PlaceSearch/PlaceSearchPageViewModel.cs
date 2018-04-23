using AppCursoXamarinDevsDNA.Base;
using AppCursoXamarinDevsDNA.Services.NavigationService;
using AppCursoXamarinDevsDNA.Services.PlaceAutocomplete;
using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppCursoXamarinDevsDNA.Features.PlaceSearch
{
    public class PlaceSearchPageViewModel : BaseViewModel
    {
        private ReactiveCommand _backButtonCommand;
        public ReactiveCommand BackButtonCommand => _backButtonCommand;

        private ReactiveCommand _autoCompletePlacesCommand;
        public ReactiveCommand AutoCompletePlacesCommand => _autoCompletePlacesCommand;

        private AutocompletePlace _selectInMapDefaultPlace;

        #region BINDABLE PROPERTIES

        private AutocompletePlace _selectedItem;
        public AutocompletePlace SelectedItem
        {
            get { return _selectedItem; }
            set { this.RaiseAndSetIfChanged(ref _selectedItem, value); }
        }

        private List<AutocompletePlace> _placesList;
        public List<AutocompletePlace> PlacesList
        {
            get { return _placesList; }
            set { this.RaiseAndSetIfChanged(ref _placesList, value); }
        }
        #endregion

        public PlaceSearchPageViewModel()
        {
            _backButtonCommand = ReactiveCommand.Create(BackButtonCommandAsync);
            _autoCompletePlacesCommand = ReactiveCommand.Create<object>(param => AutoCompletePlaces(param));
        }

        public override void Load(NavigationParameters navigationParameters)
        {
            base.Load(navigationParameters);

            _selectInMapDefaultPlace = new AutocompletePlace()
            {
                ID = "select_in_map",
                Description = "Selección en mapa"
            };
            PlacesList = new List<AutocompletePlace>() { _selectInMapDefaultPlace };
        }

        #region COMMAND HANDLERS
        private void BackButtonCommandAsync()
        {
            this.NavigateBack(null);
        }

        private void AutoCompletePlaces(object parameter)
        {
            var placesList = (List<AutocompletePlace>)parameter;

            if (placesList != null)
            {
                placesList.Insert(0, _selectInMapDefaultPlace); // Insert at 0 index Select in Map option
                PlacesList = placesList;
            }
        }

        public ICommand ItemTappedCommand => (new Command(
          (object sender) =>
          {
              var place = ((AutocompletePlace)sender);

              NavigationParameters navigatonParameters = new NavigationParameters();
              navigatonParameters.Add("place-selected", place);

              SelectedItem = null;

              this.NavigateBack(navigatonParameters);
          }));
        #endregion
    }
}
