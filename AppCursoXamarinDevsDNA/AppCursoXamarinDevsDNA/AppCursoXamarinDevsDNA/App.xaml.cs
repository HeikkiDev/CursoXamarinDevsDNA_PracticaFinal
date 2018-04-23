using System;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using AppCursoXamarinDevsDNA.Services.NavigationService;
using AppCursoXamarinDevsDNA.Services.Analytics;
using AppCursoXamarinDevsDNA.Services.AppProperties;
using ReactiveUI;
using Splat;
using AppCursoXamarinDevsDNA.Services.NearbyCinemas;

namespace AppCursoXamarinDevsDNA
{
    public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            RegisterViewModels();
            RegisterServices();

            var appPropertiesService = Splat.Locator.Current.GetService<IAppPropertiesService>();

            if (appPropertiesService.ContainsKey("user_login_token"))
            {
                MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                MainPage = new NavigationPage(new Features.Login.LoginPage());
            }
        }

        /// <summary>
        /// Registro de servicios
        /// </summary>
        private void RegisterServices()
        {
            Splat.Locator.CurrentMutable.Register(() => new NavigationService(), typeof(INavigationService));
            Splat.Locator.CurrentMutable.Register(() => new AnalyticsService(), typeof(IAnalyticsService));
            Splat.Locator.CurrentMutable.Register(() => new AppPropertiesService(), typeof(IAppPropertiesService));
            Splat.Locator.CurrentMutable.Register(() => new Features.Login.LoginService(), typeof(Features.Login.ILoginService));
            Splat.Locator.CurrentMutable.Register(() => new NearbyCinemasService(), typeof(INearbyCinemasService));
        }

        /// <summary>
        /// Registro de Views y sus respectivos ViewModels en Splat, 
        /// y los busca automáticamente mediante Reflection
        /// </summary>
        public void RegisterViewModels()
        {
            Assembly assembly = this.GetType().GetTypeInfo().Assembly;

            var viewsInfo = assembly.DefinedTypes
                                .Where(dt => !dt.IsAbstract &&
                                       dt.ImplementedInterfaces.Any(typeView => typeView == typeof(IViewFor)));

            foreach (TypeInfo viewInfo in viewsInfo)
            {
                Type viewForType = viewInfo.ImplementedInterfaces
                    .FirstOrDefault(interfaceImplemented => interfaceImplemented.IsConstructedGenericType &&
                                                            interfaceImplemented.GetGenericTypeDefinition() == typeof(IViewFor<>));

                Func<object> viewConstructor = () => Activator.CreateInstance(viewInfo.AsType());
                var viewModel = viewForType.GenericTypeArguments[0];

                Splat.Locator.CurrentMutable.Register(viewConstructor, viewModel);
            }
        }

        protected override void OnStart ()
		{
            // Analytics and Crashes for Android and iOS with AppCenter
            AppCenter.Start(
                "android=1d582377-3d4f-47d6-a4dc-9afd97550639;"
                + "ios=70488661-d6e9-4988-9796-e199c4bd42bd", 
                typeof(Analytics), 
                typeof(Crashes));
        }

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
