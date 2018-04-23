using AppCursoXamarinDevsDNA.Services.NavigationService;
using ReactiveUI;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCursoXamarinDevsDNA
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage
	{
		public MainPage(NavigationParameters navigationParameters = null) : base(navigationParameters)
		{
			InitializeComponent();
		}

        public override void CreateBindings(Action<IDisposable> d)
        {
            base.CreateBindings(d);

            //TODO: Ver cómo hacer el Binding al ToolbarItem, me está fallando
            //d(this.BindCommand(ViewModel, vm => vm.ToolbarItemCommand, v => v.logoutToolbar));

            d(this.OneWayBind(ViewModel, vm => vm.CenterPosition, v => v.mapSelectPlace.CenterPosition));
            d(this.OneWayBind(ViewModel, vm => vm.CurrentPinPlace, v => v.mapSelectPlace.PinPlace));
            d(this.Bind(ViewModel, vm => vm.CurrentPinPlaceDetails, v => v.mapSelectPlace.PinPlaceDetails));
            d(this.Bind(ViewModel, vm => vm.SelectionType, v => v.mapSelectPlace.PinSelectionType));
            d(this.OneWayBind(ViewModel, vm => vm.MapTappedCommand, v => v.mapSelectPlace.MapClickCommand));

            d(this.OneWayBind(ViewModel, vm => vm.IsMapSelectionEnable, v => v.frameSearchMode.IsVisible, (isVisible) => !isVisible));

            //TODO: Ver cómo hacer bind al command de este TapGesture desde aquí
            //var tapGestureRecognizer = new TapGestureRecognizer();
            //frameSearchMode.GestureRecognizers.Add(tapGestureRecognizer);
            //d(this.BindCommand(ViewModel, vm => vm.SearchTappedCommand, v => v.frameSearchMode, );

            d(this.OneWayBind(ViewModel, vm => vm.TextSearched, v => v.labelSearchedText.Text));

            d(this.OneWayBind(ViewModel, vm => vm.IsMapSelectionEnable, v => v.frameInMapMode.IsVisible));

            d(this.BindCommand(ViewModel, vm => vm.NavigateToMoviesCommand, v => v.buttonShowMovies));
            d(this.OneWayBind(ViewModel, vm => vm.IsVisibleShowMoviesButton, v => v.buttonShowMovies.IsVisibleWithBottomAnimation));

            d(this.OneWayBind(ViewModel, vm => vm.IsMapSelectionEnable, v => v.stacklayoutInMapMode.IsVisible));

            d(this.BindCommand(ViewModel, vm => vm.CloseMapSelectionCommand, v => v.buttonCloseInMapMode));
            d(this.BindCommand(ViewModel, vm => vm.OkMapSelectionCommand, v => v.buttonOkInMapMode));
            d(this.OneWayBind(ViewModel, vm => vm.IsEnabledOkMapSelection, v => v.buttonOkInMapMode.IsEnabled));
        }
    }
}
