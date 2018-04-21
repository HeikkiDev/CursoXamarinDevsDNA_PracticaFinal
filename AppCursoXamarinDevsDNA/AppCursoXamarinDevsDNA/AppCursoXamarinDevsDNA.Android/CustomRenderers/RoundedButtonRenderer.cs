using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Button), typeof(AppCursoXamarinDevsDNA.Droid.CustomRenderers.RoundedButtonRenderer))]
namespace AppCursoXamarinDevsDNA.Droid.CustomRenderers
{
    /// <summary>
    /// Custom Renderer para que la propiedad BorderRadius funcione en Android
    /// </summary>
    public class RoundedButtonRenderer : ButtonRenderer
    {
        public RoundedButtonRenderer(Context context) : base(context)
        {

        }
    }
}