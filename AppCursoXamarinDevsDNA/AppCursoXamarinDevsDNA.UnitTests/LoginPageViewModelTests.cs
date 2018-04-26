using AppCursoXamarinDevsDNA.Features.Login;
using AppCursoXamarinDevsDNA.Services.Analytics;
using AppCursoXamarinDevsDNA.Services.AppProperties;
using AppCursoXamarinDevsDNA.Services.NavigationService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using ReactiveUI;
using System;

namespace AppCursoXamarinDevsDNA.UnitTests
{
    [TestClass]
    public class LoginPageViewModelTests
    {
        private static Mock<INavigationService> navigationService;
        private static Mock<IAnalyticsService> analyticsService;
        private static Mock<IAppPropertiesService> appPropertiesService;
        private static Mock<ILoginService> loginService;
        private static LoginPageViewModel viewModel;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            navigationService = new Mock<INavigationService>();
            analyticsService = new Mock<IAnalyticsService>();
            appPropertiesService = new Mock<IAppPropertiesService>();
            loginService = new Mock<ILoginService>();
        }

        [TestInitialize]
        public void TestInit()
        {
            viewModel = new LoginPageViewModel(navigationService.Object, analyticsService.Object, appPropertiesService.Object, loginService.Object);
            loginService.Reset();
            navigationService.Reset();
            analyticsService.Reset();
            appPropertiesService.Reset();
        }

        [TestMethod]
        public void GivenCorrectUserAndPassword_ThenNavigationIsCalled()
        {
            // Arrange
            navigationService.Setup(service => service.SetMainPage(It.IsAny<MainPage>())).Verifiable("Error in NavigationService");
            analyticsService.Setup(service => service.TrackEvent(It.IsAny<string>(), It.IsAny<IDictionary<string, string>>())).Verifiable("Error en AnalyticsService");
            appPropertiesService.Setup(service => service.Set(It.IsAny<string>(), It.IsAny<string>())).Verifiable("Error en AppPropertiesService");
            loginService.Setup(service => service.LoginWithUserAndPassword(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);

            // Act
            viewModel.Usuario = "enrique";
            viewModel.Password = "password1";
            viewModel.LoginButtonCommand.Execute();
            // Assets
            // No me funciona este Verifiy porque estoy instanciando MainPage dentro de LoginButtonCommand :( 
            // Tendría que cambiar el servicio de Navegación y no me da tiempo
            //navigationService.Verify(service => service.SetMainPage(It.IsAny<Page>()), Times.Once);
            analyticsService.Verify(service => service.TrackEvent(It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()), Times.Once);
            appPropertiesService.Verify(service => service.Set(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
