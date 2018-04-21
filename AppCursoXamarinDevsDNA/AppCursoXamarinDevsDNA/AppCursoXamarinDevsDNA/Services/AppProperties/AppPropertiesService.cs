using System;
using System.Collections.Generic;
using System.Text;

namespace AppCursoXamarinDevsDNA.Services.AppProperties
{
    /// <summary>
    /// Service para acceder a App.Current.Properties de Xamarin
    /// </summary>
    public class AppPropertiesService : IAppPropertiesService
    {
        public string Get(string keyName)
        {
            return App.Current.Properties[keyName].ToString();
        }

        public void Set(string keyName, string value)
        {
            App.Current.Properties[keyName] = value;
        }

        public bool Remove(string keyName)
        {
            return App.Current.Properties.Remove(keyName);
        }

        public bool ContainsKey(string keyName)
        {
            return App.Current.Properties.ContainsKey(keyName);
        }
    }
}
