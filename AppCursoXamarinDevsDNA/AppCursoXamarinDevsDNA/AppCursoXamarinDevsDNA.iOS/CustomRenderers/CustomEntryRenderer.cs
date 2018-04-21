using CoreAnimation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using CoreGraphics;
using AppCursoXamarinDevsDNA.CustomControls;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(AppCursoXamarinDevsDNA.iOS.CustomRenderers.CustomEntryRenderer))]
namespace AppCursoXamarinDevsDNA.iOS.CustomRenderers
{
    class CustomEntryRenderer : EntryRenderer
    {
        private CALayer _line;

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            _line = null;

            if (Control == null || e.NewElement == null)
                return;

            var element = (CustomEntry)Element;
            var customColor = element.BorderColor.ToCGColor();
            Control.BorderStyle = UITextBorderStyle.None;

            _line = new CALayer
            {
                BorderColor = customColor,
                Frame = new CGRect(0, Frame.Height / 2, Frame.Width * 2, 1f)
            };

            Control.Layer.AddSublayer(_line);
        }
    }
}