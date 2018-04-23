using AppCursoXamarinDevsDNA.CustomControls;
using AppCursoXamarinDevsDNA.CustomControls.SelectPlaceMap;
using MapKit;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SelectPlaceMap), typeof(AppCursoXamarinDevsDNA.iOS.CustomRenderers.SelectPlaceMapRenderer))]
namespace AppCursoXamarinDevsDNA.iOS.CustomRenderers
{
    public class SelectPlaceMapRenderer : MapRenderer
    {
        private UITapGestureRecognizer _uIGestureRecognizer;

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                var nativeMap = Control as MKMapView;
                if (_uIGestureRecognizer != null)
                    nativeMap.RemoveGestureRecognizer(_uIGestureRecognizer);
            }

            if (e.NewElement != null)
            {
                var nativeMap = Control as MKMapView;
                if (_uIGestureRecognizer == null)
                {
                    _uIGestureRecognizer = new UITapGestureRecognizer(OnMapTapped);
                    nativeMap.AddGestureRecognizer(_uIGestureRecognizer);
                }
            }
        }

        private void OnMapTapped(UITapGestureRecognizer recognizer)
        {
            var cgPoint = recognizer.LocationInView(Control);
            var location = ((MKMapView)Control).ConvertPoint(cgPoint, Control);

            SelectPlaceMap selectPlaceMap = (SelectPlaceMap)Element;
            selectPlaceMap.OnMapTapped(new Position(location.Latitude, location.Longitude));
        }
    }
}