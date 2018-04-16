using AppCursoXamarinDevsDNA.Services.NavigationService;
using ReactiveUI;
using ReactiveUI.XamForms;
using System;

namespace AppCursoXamarinDevsDNA.Base
{
    public class BaseReactiveContentPage<TViewModel> : ReactiveContentPage<TViewModel> where TViewModel : BaseViewModel
    {
        public BaseReactiveContentPage(NavigationParameters navigationParameters = null)
        {
            ViewModel = Utils.ExpressionUtils.Instance<TViewModel>();

            this.WhenActivated(registerDisposable => CreateBindings(registerDisposable));

            ViewModel.Load(navigationParameters);
        }

        public virtual void CreateBindings(Action<IDisposable> registerDisposable)
        {

        }
    }
}
