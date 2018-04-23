using AppCursoXamarinDevsDNA.Services.NavigationService;
using ReactiveUI;
using System;
using Xamarin.Forms.Xaml;

namespace AppCursoXamarinDevsDNA.Features.NearbyCinemas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NearbyCinemasPage
	{
		public NearbyCinemasPage (NavigationParameters navigationParameters = null) : base(navigationParameters)
		{
			InitializeComponent ();
		}

        public override void CreateBindings(Action<IDisposable> d)
        {
            base.CreateBindings(d);

            d(this.OneWayBind(ViewModel, vm => vm.CinemasList, v => v.listViewNearbyCinemas.ItemsSource));
            d(this.Bind(ViewModel, vm => vm.SelectedItem, v => v.listViewNearbyCinemas.SelectedItem));
        }
    }
}