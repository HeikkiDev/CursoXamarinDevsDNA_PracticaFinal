using Android.Content;
using Android.Gms.Maps;
using AppCursoXamarinDevsDNA.CustomControls.SelectPlaceMap;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;

[assembly: ExportRenderer(typeof(SelectPlaceMap), typeof(AppCursoXamarinDevsDNA.Droid.CustomRenderers.SelectPlaceMapRenderer))]
namespace AppCursoXamarinDevsDNA.Droid.CustomRenderers
{
    public class SelectPlaceMapRenderer : MapRenderer
    {
        private Context _context;

        public SelectPlaceMapRenderer(Context context) : base(context)
        {
            _context = context;
        }

        ~SelectPlaceMapRenderer()
        {
            NativeMap.MapClick -= OnMapClick;
        }

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                Control.GetMapAsync(this);
            }
        }

        protected override void OnMapReady(GoogleMap map)
        {
            base.OnMapReady(map);
            map.UiSettings.ZoomControlsEnabled = false; // Disable zoom buttons
            NativeMap.MapClick -= OnMapClick;
            NativeMap.MapClick += OnMapClick;
        }

        private void OnMapClick(object sender, GoogleMap.MapClickEventArgs e)
        {
            SelectPlaceMap selectPlaceMap = (SelectPlaceMap)Element;
            selectPlaceMap.OnMapTapped(new Position(e.Point.Latitude, e.Point.Longitude));
        }
    }
}