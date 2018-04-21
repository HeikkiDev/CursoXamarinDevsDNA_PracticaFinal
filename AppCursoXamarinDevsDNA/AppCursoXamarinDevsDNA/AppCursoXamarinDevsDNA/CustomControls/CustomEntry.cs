using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppCursoXamarinDevsDNA.CustomControls
{
    /// <summary>
    /// Custom Control Entry, para usar los custom renderers que permiten definir un borde inferior de color (por defecto dark gray)
    /// </summary>
    public class CustomEntry : Entry
    {
        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set
            {
                SetValue(BorderColorProperty, value);
            }
        }

        public static readonly BindableProperty BorderColorProperty = 
            BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(CustomEntry), Color.DarkGray);
    }
}
