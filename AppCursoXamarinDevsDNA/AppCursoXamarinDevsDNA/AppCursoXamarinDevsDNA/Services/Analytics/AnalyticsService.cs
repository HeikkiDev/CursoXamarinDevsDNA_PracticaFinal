using System.Collections.Generic;

namespace AppCursoXamarinDevsDNA.Services.Analytics
{
    /// <summary>
    /// Service para acceder a las funciones del paquete Microsoft.AppCenter.Analytics
    /// </summary>
    public class AnalyticsService : IAnalyticsService
    {
        public AnalyticsService()
        {

        }

        public void TrackEvent(string eventName, IDictionary<string, string> properties = null)
        {
            if (properties != null)
                Microsoft.AppCenter.Analytics.Analytics.TrackEvent(eventName, properties);
            else
                Microsoft.AppCenter.Analytics.Analytics.TrackEvent(eventName);
        }
    }
}
