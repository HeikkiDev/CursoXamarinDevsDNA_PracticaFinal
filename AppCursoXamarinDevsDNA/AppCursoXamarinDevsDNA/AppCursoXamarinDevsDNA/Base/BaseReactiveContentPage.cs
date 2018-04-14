using ReactiveUI;
using ReactiveUI.XamForms;
using System;

namespace AppCursoXamarinDevsDNA.Base
{
    public class BaseReactiveContentPage<TViewModel> : ReactiveContentPage<TViewModel> where TViewModel : BaseViewModel
    {
        public BaseReactiveContentPage()
        {
            ViewModel = Utils.ExpressionUtils.Instance<TViewModel>();

            this.WhenActivated(registerDisposable => CreateBindings(registerDisposable));
        }

        public virtual void CreateBindings(Action<IDisposable> registerDisposable)
        {

        }
    }
}
