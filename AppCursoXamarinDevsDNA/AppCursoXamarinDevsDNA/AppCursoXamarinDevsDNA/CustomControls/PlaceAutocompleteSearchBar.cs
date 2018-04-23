using AppCursoXamarinDevsDNA.Services.PlaceAutocomplete;
using Splat;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppCursoXamarinDevsDNA.CustomControls
{
    public class PlaceAutocompleteSearchBar : SearchBar
    {
        private CancellationTokenSource _cancellationTokenSource;
        private IPlaceAutocompleteService _placeAutocompleteService;

        public PlaceAutocompleteSearchBar()
        {
            _placeAutocompleteService = DependencyService.Get<IPlaceAutocompleteService>();

            this.TextChanged -= PlaceAutocompleteSearchBar_TextChanged;
            this.TextChanged += PlaceAutocompleteSearchBar_TextChanged;
        }

        ~PlaceAutocompleteSearchBar()
        {
            this.TextChanged -= PlaceAutocompleteSearchBar_TextChanged;
        }

        /// <summary>
		/// Gets or sets the minimum search text.
        /// Default value is 3.
		/// </summary>
		/// <value>The minimum search text.</value>
        private int _minimumSearchText = 3;
        public int MinimumSearchText
        {
            get
            {
                return _minimumSearchText;
            }
            set
            {
                _minimumSearchText = value;
            }
        }

        /// <summary>
        /// Gets or sets the AutocompleteCommand.
        /// </summary>
        public ICommand AutocompleteCommand
        {
            get { return (ICommand)GetValue(AutocompleteCommandProperty); }
            set { SetValue(AutocompleteCommandProperty, value); }
        }

        /// <summary>
        /// The AutocompleteCommand bindable property.
        /// </summary>
        public static BindableProperty AutocompleteCommandProperty =
            BindableProperty.Create(nameof(AutocompleteCommand), typeof(ICommand), typeof(PlaceAutocompleteSearchBar), null, BindingMode.TwoWay);


        private async void PlaceAutocompleteSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue) && e.NewTextValue.Length >= _minimumSearchText)
            {
                try
                {
                    // Cancel previous search and reset timer
                    _cancellationTokenSource?.Cancel();
                }
                catch (ObjectDisposedException) { }

                using (_cancellationTokenSource = new CancellationTokenSource())
                {
                    try
                    {
                        await Task.Delay(TimeSpan.FromMilliseconds(100), _cancellationTokenSource.Token);

                        _cancellationTokenSource.Token.ThrowIfCancellationRequested();
                        var placesList = await _placeAutocompleteService.GetPlaces(e.NewTextValue, _cancellationTokenSource.Token);
                        _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                        if (placesList != null)
                            ExecuteAutocompleteCommand(placesList);
                        else
                            ExecuteAutocompleteCommand(new List<AutocompletePlace>());
                    }
                    catch (ObjectDisposedException)
                    {
                        //
                    }
                    catch (OperationCanceledException)
                    {
                        // if the operation is cancelled, do nothing
                    }
                }
            }
            else
            {
                ExecuteAutocompleteCommand(new List<AutocompletePlace>());
            }
        }

        private void ExecuteAutocompleteCommand(List<AutocompletePlace> placesList)
        {
            if (AutocompleteCommand != null && AutocompleteCommand.CanExecute(null))
            {
                AutocompleteCommand.Execute(placesList);
            }
        }
    }
}
