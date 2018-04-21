namespace AppCursoXamarinDevsDNA.Services.AppProperties
{
    public interface IAppPropertiesService
    {
        bool ContainsKey(string keyName);
        string Get(string keyName);
        bool Remove(string keyName);
        void Set(string keyName, string value);
    }
}