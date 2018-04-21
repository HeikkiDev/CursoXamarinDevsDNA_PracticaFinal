using AppCursoXamarinDevsDNA.Services.NavigationService;
using ReactiveUI;
using System;
using Xamarin.Forms.Xaml;

namespace AppCursoXamarinDevsDNA.Features.Login
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage
	{
		public LoginPage(NavigationParameters navigationParameters = null) : base(navigationParameters)
        {
            InitializeComponent();
        }

        public override void CreateBindings(Action<IDisposable> d)
        {
            base.CreateBindings(d);

            d(this.OneWayBind(ViewModel, vm => vm.IsBusy, v => v.entryUser.IsEnabled, (isEnabled) => !isEnabled));
            d(this.OneWayBind(ViewModel, vm => vm.IsBusy, v => v.entryPassword.IsEnabled, (isEnabled) => !isEnabled));
            d(this.OneWayBind(ViewModel, vm => vm.IsLoginErrorLabelVisible, v => v.labelWrongUserOrPass.IsVisibleCustom));
            d(this.OneWayBind(ViewModel, vm => vm.IsBusy, v => v.activityIndicator.IsRunning));
            d(this.OneWayBind(ViewModel, vm => vm.IsBusy, v => v.buttonLogin.IsEnabled, (isEnabled) => !isEnabled));
            d(this.BindCommand(ViewModel, vm => vm.LoginButtonCommand, v => v.buttonLogin));
        }
    }
}