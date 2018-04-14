using ReactiveUI;
using System;
using System.Reactive.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCursoXamarinDevsDNA
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

        public override void CreateBindings(Action<IDisposable> registerDisposable)
        {
            base.CreateBindings(registerDisposable);

            //TODO Bindings
        }
    }
}
