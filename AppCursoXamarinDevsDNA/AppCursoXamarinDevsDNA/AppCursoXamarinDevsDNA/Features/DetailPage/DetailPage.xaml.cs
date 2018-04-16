using AppCursoXamarinDevsDNA.Services.NavigationService;
using System;
using Xamarin.Forms.Xaml;
using ReactiveUI;

namespace AppCursoXamarinDevsDNA.Features.DetailPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetailPage
	{
		public DetailPage (NavigationParameters navigationParameters = null) : base(navigationParameters)
        {
			InitializeComponent ();
		}

        public override void CreateBindings(Action<IDisposable> d)
        {
            base.CreateBindings(d);

            d(this.Bind(ViewModel, vm => vm.LabelText, v => v.labelTexto.Text));
        }
    }
}