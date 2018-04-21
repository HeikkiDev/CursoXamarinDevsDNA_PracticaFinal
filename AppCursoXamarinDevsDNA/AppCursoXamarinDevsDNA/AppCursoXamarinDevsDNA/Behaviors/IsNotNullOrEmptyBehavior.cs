using AppCursoXamarinDevsDNA.CustomControls;
using Xamarin.Forms;

namespace AppCursoXamarinDevsDNA.Behaviors
{
    /// <summary>
    /// Behavior para el control CustomEntry que comprueba el texto en cada evento TextChanged y 
    /// si es vacío o espacios en blanco cambia el color del texto y del borde del control a Color.Red.
    /// En caso contrario TextColor -> Color.Default y BorderColor -> Color.DarkGray
    /// </summary>
    public class IsNotNullOrEmptyBehavior : Behavior<CustomEntry>
    {
        protected override void OnAttachedTo(CustomEntry bindable)
        {
            bindable.TextChanged += HandleTextChanged;
            base.OnAttachedTo(bindable);
        }

        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            bool IsValid = !string.IsNullOrWhiteSpace(e.NewTextValue);
            CustomEntry entry = ((CustomEntry)sender);
            entry.TextColor = IsValid ? Color.Default : Color.Red;
            entry.BorderColor = IsValid ? Color.DarkGray : Color.Red;
        }

        protected override void OnDetachingFrom(CustomEntry bindable)
        {
            bindable.TextChanged -= HandleTextChanged;
            base.OnDetachingFrom(bindable);
        }
    }
}
