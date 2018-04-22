using AppCursoXamarinDevsDNA.Services.NavigationService;
using ReactiveUI;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCursoXamarinDevsDNA.Features.PlaceSearch
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlaceSearchPage
    {
        public PlaceSearchPage(NavigationParameters navigationParameters = null) : base(navigationParameters)
        {
            InitializeComponent();
        }

        public override void CreateBindings(Action<IDisposable> d)
        {
            base.CreateBindings(d);

            d(this.OneWayBind(ViewModel, vm => vm.AutoCompletePlacesCommand, v => v.autocompleteSearchBar.AutocompleteCommand));

            d(this.OneWayBind(ViewModel, vm => vm.PlacesList, v => v.listViewPlacesAutocomplete.ItemsSource));
            d(this.Bind(ViewModel, vm => vm.SelectedItem, v => v.listViewPlacesAutocomplete.SelectedItem));

            // TODO: Ver cómo hacer el Binding del Label que está dentro del ListView

            //Focus a la barra de búsqueda
            autocompleteSearchBar.Focus();
        }
    }
}