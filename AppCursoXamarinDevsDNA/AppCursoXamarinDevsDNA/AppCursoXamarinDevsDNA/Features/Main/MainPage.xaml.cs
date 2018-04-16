using AppCursoXamarinDevsDNA.Services.NavigationService;
using ReactiveUI;
using System;
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

            d(this.BindCommand(ViewModel, vm => vm.NavigateToDetailCommand, v => v.BtnPushView));

            //TODO Bindings
        }
    }
}
