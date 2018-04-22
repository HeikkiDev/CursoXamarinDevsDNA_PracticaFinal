using AppCursoXamarinDevsDNA.Services.NavigationService;
using ReactiveUI;
using ReactiveUI.XamForms;
using System;

namespace AppCursoXamarinDevsDNA.Base
{
    public class BaseReactiveContentPage<TViewModel> : ReactiveContentPage<TViewModel> where TViewModel : BaseViewModel
    {
        public event EventHandler<NavigationParameters> OnBackNavigation;

        public BaseReactiveContentPage(NavigationParameters navigationParameters = null)
        {
            ViewModel = Utils.ExpressionUtils.Instance<TViewModel>();

            this.WhenActivated(registerDisposable => CreateBindings(registerDisposable));

            ViewModel.Load(navigationParameters);
            ViewModel.BackNavigation += ViewModel_BackNavigation;
        }

        protected override void OnDisappearing()
        {
            ViewModel.BackNavigation -= ViewModel_BackNavigation;
            base.OnDisappearing();
        }

        private void ViewModel_BackNavigation(object sender, NavigationParameters navigationParameters)
        {
            OnBackNavigation(this, navigationParameters);
        }

        public virtual void CreateBindings(Action<IDisposable> registerDisposable)
        {

        }
    }
}
