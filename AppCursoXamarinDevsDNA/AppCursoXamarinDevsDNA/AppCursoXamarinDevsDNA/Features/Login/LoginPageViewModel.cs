using AppCursoXamarinDevsDNA.Base;
using AppCursoXamarinDevsDNA.Services.Analytics;
using AppCursoXamarinDevsDNA.Services.AppProperties;
using AppCursoXamarinDevsDNA.Services.NavigationService;
using ReactiveUI;
using Splat;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppCursoXamarinDevsDNA.Features.Login
{
    public class LoginPageViewModel : BaseViewModel
    {
        private readonly ILoginService _loginService;

        private ReactiveCommand _loginButtonCommand;
        public ReactiveCommand LoginButtonCommand => _loginButtonCommand;

        private string _usuario;
        public string Usuario
        {
            get => _usuario;
            set => this.RaiseAndSetIfChanged(ref _usuario, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        private bool _isLoginErrorLabelVisible;
        public bool IsLoginErrorLabelVisible
        {
            get => _isLoginErrorLabelVisible;
            set => this.RaiseAndSetIfChanged(ref _isLoginErrorLabelVisible, value);
        }

        public LoginPageViewModel(): this(null, null, null, null)
        {
            // Constructor vacío para instanciar con la herramienta de reflection de Utils
        }

        public LoginPageViewModel(INavigationService navigationService, IAnalyticsService analyticsService, IAppPropertiesService appPropertiesService, ILoginService loginService)
            :base(navigationService, analyticsService, appPropertiesService)
        {
            _loginService = loginService ?? Locator.Current.GetService<ILoginService>();

            // Para deshabilitar el botón de Login si el Usuario o la Password están vacíos
            var canExecute = this.WhenAnyValue(
                                            vm => vm.Usuario,
                                            vm => vm.Password,
                                            (e, p) => !string.IsNullOrEmpty(e) && !string.IsNullOrEmpty(p));

            _loginButtonCommand = ReactiveCommand.CreateFromTask(LoginButtonTappedAsync, canExecute);
        }

        public override void Load(NavigationParameters navigationParameters)
        {
            base.Load(navigationParameters);

            //
        }

        private async Task LoginButtonTappedAsync()
        {
            IsLoginErrorLabelVisible = false;
            IsBusy = true;

            bool loginResult = await _loginService.LoginWithUserAndPassword(Usuario, Password);

            if (loginResult)
            {
                AppPropertiesService.Set("user_login_token", Usuario);

                AnalyticsService.TrackEvent(AppCenterEvents.USER_LOG_IN, new Dictionary<string, string> {
                        { "userId", Usuario }
                  });

                NavigationService.SetMainPage(new MainPage());
            }
            else
            {
                IsLoginErrorLabelVisible = true;
            }

            IsBusy = false;
        }
    }
}
