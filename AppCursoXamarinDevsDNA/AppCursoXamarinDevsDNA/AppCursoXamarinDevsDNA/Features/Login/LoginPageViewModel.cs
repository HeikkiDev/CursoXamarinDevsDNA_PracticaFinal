using AppCursoXamarinDevsDNA.Base;
using AppCursoXamarinDevsDNA.Services.Analytics;
using AppCursoXamarinDevsDNA.Services.NavigationService;
using ReactiveUI;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppCursoXamarinDevsDNA.Features.Login
{
    public class LoginPageViewModel : BaseViewModel
    {
        private ReactiveCommand _loginButtonCommand;
        public ReactiveCommand LoginButtonCommand => _loginButtonCommand;

        private string _email;
        public string Email
        {
            get => _email;
            set => this.RaiseAndSetIfChanged(ref _email, value);
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

        public LoginPageViewModel()
        {
            _loginButtonCommand = ReactiveCommand.CreateFromTask(LoginButtonTappedAsync);
        }

        public override void Load(NavigationParameters navigationParameters)
        {
            base.Load(navigationParameters);

            //
        }

        private async Task LoginButtonTappedAsync()
        {
            IsBusy = true;

            //TODO: Hacer un check falso del email y password, y guardar email como token de inicio de sesión
            //Simular petición de Login
            await Task.Delay(3000);

            AnalyticsService.TrackEvent(AppCenterEvents.USER_LOG_IN, new Dictionary<string, string> {
                        { "userId", Email }
                  });

            NavigationService.SetMainPage(new MainPage());
        }
    }
}
