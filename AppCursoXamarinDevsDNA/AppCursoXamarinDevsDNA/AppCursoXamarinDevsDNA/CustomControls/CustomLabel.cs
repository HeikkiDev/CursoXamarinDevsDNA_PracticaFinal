using Xamarin.Forms;

namespace AppCursoXamarinDevsDNA.CustomControls
{
    public class CustomLabel : Label
    {
        public bool IsVisibleCustom
        {
            get { return (bool)GetValue(IsVisibleCustomProperty); }
            set
            {
                SetValue(IsVisibleCustomProperty, value);
            }
        }

        public static readonly BindableProperty IsVisibleCustomProperty = BindableProperty.Create(nameof(IsVisibleCustom), typeof(bool), typeof(CustomLabel), false, BindingMode.Default, propertyChanged: OnIsVisiblecustomChanged);


        static void OnIsVisiblecustomChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var label = bindable as CustomLabel;
            if ((bool)oldValue == false && (bool)newValue)
            {
                label.FadeTo(1, 500, Easing.CubicIn); // Cambia la Opacity a 100% en un tiempo de 0,5 segundos
            }
            if ((bool)oldValue && (bool)newValue == false)
            {
                label.FadeTo(0, 50, Easing.CubicIn); // Cambia la Opacity a 0% en un tiempo de 0,05 segundos
            }
        }
    }
}
