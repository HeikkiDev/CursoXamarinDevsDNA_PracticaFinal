using AppCursoXamarinDevsDNA.Features.Main;
using AppCursoXamarinDevsDNA.Services.NavigationService;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Xamarin.Forms;

namespace AppCursoXamarinDevsDNA
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            RegisterViewModels();
            RegisterServices();

            MainPage = new NavigationPage(new MainPage());
        }

        /// <summary>
        /// Registro de servicios
        /// </summary>
        private void RegisterServices()
        {
            Splat.Locator.CurrentMutable.Register(() => new NavigationService(), typeof(INavigationService));
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
			// Handle when your app starts
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
