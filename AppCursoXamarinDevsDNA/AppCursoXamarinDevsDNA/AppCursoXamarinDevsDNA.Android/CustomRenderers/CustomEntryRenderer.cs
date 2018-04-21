using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using AppCursoXamarinDevsDNA.CustomControls;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(AppCursoXamarinDevsDNA.Droid.CustomRenderers.CustomEntryRenderer))]
namespace AppCursoXamarinDevsDNA.Droid.CustomRenderers
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control == null || Element == null) return;

            if (e.PropertyName == CustomEntry.BorderColorProperty.PropertyName)
            {
                var element = (CustomEntry)Element;
                var customColor = element.BorderColor.ToAndroid();

                if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                    Control.BackgroundTintList = ColorStateList.ValueOf(customColor);
                else
                    Control.Background.SetColorFilter(customColor, PorterDuff.Mode.SrcAtop);
            }
        }
    }
}