using AppCursoXamarinDevsDNA.Services.PlaceAutocomplete;
using Splat;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace AppCursoXamarinDevsDNA.CustomControls
{
    public class SelectPlaceMap : Map
    {
        public enum SelectionType
        {
            None = 0,
            SEARCHBAR = 1,
            IN_MAP = 2
        }
        
        private CancellationTokenSource _cancellationTokenSource;
        private IPlaceAutocompleteService _placeAutocompleteService;
        private App _app => (App)Xamarin.Forms.Application.Current;

        public SelectPlaceMap()
        {
            _placeAutocompleteService = Locator.Current.GetService<IPlaceAutocompleteService>();
        }
        public SelectPlaceMap(IPlaceAutocompleteService placeAutocompleteService)
        {
            _placeAutocompleteService = placeAutocompleteService;
        }

        /// <summary>
        /// Gets or sets the Map Click command.
        /// </summary>
        public ICommand MapClickCommand
        {
            get { return (ICommand)GetValue(MapClickCommandProperty); }
            set { SetValue(MapClickCommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Map coordinates position to show
        /// </summary>
        public Position CenterPosition
        {
            get { return (Position)GetValue(CenterPositionProperty); }
            set { SetValue(CenterPositionProperty, value); }
        }

        /// <summary>
        /// Gets or sets the current Pin position
        /// </summary>
        public AutocompletePlace PinPlace
        {
            get { return (AutocompletePlace)GetValue(PinPlaceProperty); }
            set { SetValue(PinPlaceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the current Pin position Details, like coordinates
        /// </summary>
        public PlaceDetailsResponse PinPlaceDetails
        {
            get { return (PlaceDetailsResponse)GetValue(PinPlaceDetailsProperty); }
            set { SetValue(PinPlaceDetailsProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Pin Selection Type
        /// </summary>
        public SelectionType PinSelectionType
        {
            get { return (SelectionType)GetValue(PinSelectionTypeProperty); }
            set { SetValue(PinSelectionTypeProperty, value); }
        }

        /// <summary>
        /// The command property
        /// </summary>
        public static BindableProperty MapClickCommandProperty =
            BindableProperty.Create(nameof(MapClickCommand), typeof(ICommand), typeof(SelectPlaceMap), null, BindingMode.TwoWay);

        /// <summary>
        /// The center position property
        /// </summary>
        public static BindableProperty CenterPositionProperty =
            BindableProperty.Create(nameof(CenterPosition), typeof(Position), typeof(SelectPlaceMap),
                                                default(Position), BindingMode.Default, propertyChanged: OnCenterPositionChanged);

        /// <summary>
        /// The current Pin position property
        /// </summary>
        public static BindableProperty PinPlaceProperty =
            BindableProperty.Create(nameof(PinPlace), typeof(AutocompletePlace), typeof(SelectPlaceMap),
                                                default(AutocompletePlace), BindingMode.Default, propertyChanged: OnPinPlaceChanged);

        /// <summary>
        /// The current Pin Details property
        /// </summary>
        public static BindableProperty PinPlaceDetailsProperty =
            BindableProperty.Create(nameof(PinPlaceDetails), typeof(PlaceDetailsResponse), typeof(SelectPlaceMap),
                                                default(PlaceDetailsResponse), BindingMode.TwoWay);

        /// <summary>
        /// The Pin selection Type property
        /// </summary>
        public static BindableProperty PinSelectionTypeProperty =
            BindableProperty.Create(nameof(PinSelectionType), typeof(SelectionType), typeof(SelectPlaceMap),
                                                SelectionType.SEARCHBAR, BindingMode.TwoWay);


        private static void OnCenterPositionChanged(BindableObject view, object oldValue, object newValue)
        {
            var selectPlaceMap = view as SelectPlaceMap;

            if (selectPlaceMap != null)
            {
                selectPlaceMap.PositionMap();
            }
        }

        private static async void OnPinPlaceChanged(BindableObject view, object oldValue, object newValue)
        {
            var selectPlaceMap = view as SelectPlaceMap;
            var oldPlace = (oldValue != null) ? (AutocompletePlace)oldValue : null;
            var newPlace = (newValue != null) ? (AutocompletePlace)newValue : null;

            if (selectPlaceMap != null)
            {
                if (newPlace != null)
                {
                    if (selectPlaceMap.PinSelectionType == SelectionType.IN_MAP || newPlace?.ID == "select_in_map")
                    {
                        if (oldPlace != null)
                            selectPlaceMap.RemovePinFromMap();
                        return;
                    }

                    try
                    {
                        // Cancel previous search
                        selectPlaceMap._cancellationTokenSource?.Cancel();
                    }
                    catch (ObjectDisposedException) { }

                    using (selectPlaceMap._cancellationTokenSource = new CancellationTokenSource())
                    {
                        try
                        {
                            await Task.Delay(TimeSpan.FromMilliseconds(100), selectPlaceMap._cancellationTokenSource.Token);

                            selectPlaceMap._cancellationTokenSource.Token.ThrowIfCancellationRequested();

                            Position pinPosition = await selectPlaceMap.RequestDetails(newPlace);

                            selectPlaceMap._cancellationTokenSource.Token.ThrowIfCancellationRequested();

                            selectPlaceMap.AddPinToMap(pinPosition, newPlace.Description);
                        }
                        catch (OperationCanceledException)
                        {
                            // if the operation is cancelled, do nothing
                        }
                    }
                }
                else
                {
                    selectPlaceMap.RemovePinFromMap();
                }
            }
        }

        public void OnMapTapped(Position tappedPosition)
        {
            this.RemovePinFromMap(); // Clear others Pins

            if (this.PinSelectionType == SelectPlaceMap.SelectionType.IN_MAP)
            {
                this.Pins.Clear();
                // Add Pin to forms map
                this.AddPinToMap(
                    tappedPosition,
                    "Punto de búsqueda");

                // Set coordinates of added Pin
                this.PinPlaceDetails = new PlaceDetailsResponse()
                {
                    result = new PlaceDetailsResponse.Result()
                    {
                        geometry = new PlaceDetailsResponse.Geometry()
                        {
                            location = new PlaceDetailsResponse.Location()
                            {
                                lat = tappedPosition.Latitude,
                                lng = tappedPosition.Longitude
                            }
                        }
                    }
                };
            }
            else
            {
                if (this.MapClickCommand != null && this.MapClickCommand.CanExecute(false))
                    this.MapClickCommand.Execute(false);
            }
        }

        public void PositionMap()
        {
            if (CenterPosition != null)
            {
                this.MoveToRegion(
                    MapSpan.FromCenterAndRadius(
                       new Position(CenterPosition.Latitude, CenterPosition.Longitude),
                       Distance.FromKilometers(3)
                    )
                );
            }
            else
            {
                // Default position Madrid
                this.MoveToRegion(
                    MapSpan.FromCenterAndRadius(
                       new Position(40.4381311, -3.8196235),
                       Distance.FromKilometers(3)
                    )
                );
            }
        }

        public void AddPinToMap(Position pinPosition, string description)
        {
            this.Pins.Clear();
            this.Pins.Add(
                new Pin()
                {
                    Position = pinPosition,
                    Label = description
                });
            this.MoveToRegion(
                    MapSpan.FromCenterAndRadius(
                       new Position(pinPosition.Latitude, pinPosition.Longitude),
                       Distance.FromKilometers(3)
                    )
                );
        }

        public void RemovePinFromMap()
        {
            this.Pins.Clear();
        }

        private async Task<Position> RequestDetails(AutocompletePlace place)
        {
            PlaceDetailsResponse details = await _placeAutocompleteService.GetPlaceDetails(place.Place_ID, _cancellationTokenSource.Token);
            PinPlaceDetails = details; // set details so viewmodel can use the coordinates
            var lat = details?.result?.geometry?.location?.lat;
            var lng = details?.result?.geometry?.location?.lng;

            if (lat != null && lng != null)
            {
                return new Position(
                    details.result.geometry.location.lat,
                    details.result.geometry.location.lng
                );
            }
            else
                return default(Position);
        }
    }
}
