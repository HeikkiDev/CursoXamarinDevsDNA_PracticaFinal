using System.Collections.Generic;

namespace AppCursoXamarinDevsDNA.Services.Analytics
{
    public interface IAnalyticsService
    {
        void TrackEvent(string eventName, IDictionary<string, string> properties = null);
    }
}
