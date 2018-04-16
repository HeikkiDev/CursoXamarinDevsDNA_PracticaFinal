using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AppCursoXamarinDevsDNA.Services.NavigationService
{
    public class NavigationParameters
    {
        private static List<KeyValuePair<string, object>> _parameterList;

        public NavigationParameters()
        {
            _parameterList = new List<KeyValuePair<string, object>>();
        }

        /// <summary>
        /// Gets the number of parameters contained in the NavigationParameters
        /// </summary>
        public int Count
        {
            get
            {
                return _parameterList.Count;
            }
        }

        /// <summary>
        /// Gets an IEnumerable containing the keys in the NavigationParameters
        /// </summary>
        public IEnumerable<string> Keys
        {
            get { return _parameterList.Select(x => x.Key); }
        }

        /// <summary>
        /// Gets the <see cref="System.String"/> with the specified key.
        /// </summary>
        /// <returns>The value for the specified key, or <see langword="null"/> if the query does not contain such a key.</returns>
        public object this[string key]
        {
            get
            {
                foreach (var kvp in _parameterList)
                {
                    if (string.Compare(kvp.Key, key, StringComparison.Ordinal) == 0)
                    {
                        return kvp.Value;
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Adds the specified key and value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Add(string key, object value)
        {
            _parameterList.Add(new KeyValuePair<string, object>(key, value));
        }

        /// <summary>
        /// Determines whether the NavigationParameters contains the specified key
        /// </summary>
        /// <param name="key">The key to locate</param>
        public bool ContainsKey(string key)
        {
            foreach (var kvp in _parameterList)
            {
                if (string.Compare(kvp.Key, key, StringComparison.Ordinal) == 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _parameterList.GetEnumerator();
        }

        /// <summary>
        /// Gets a strongly typed value with the specified key.
        /// </summary>
        /// <typeparam name="T">The type to cast/convert the value to.</typeparam>
        /// <param name="key">The key.</param>
        /// <returns>The value.</returns>
        public T GetValue<T>(string key)
        {
            return GetValue<T>(key, _parameterList);
        }

        private T GetValue<T>(string key, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            foreach (var kvp in parameters)
            {
                if (string.Compare(kvp.Key, key, StringComparison.Ordinal) == 0)
                {
                    if (kvp.Value == null)
                        return default(T);
                    else if (kvp.Value.GetType() == typeof(T))
                        return (T)kvp.Value;
                    else if (typeof(T).GetTypeInfo().IsAssignableFrom(kvp.Value.GetType().GetTypeInfo()))
                        return (T)kvp.Value;
                    else
                        return (T)Convert.ChangeType(kvp.Value, typeof(T));
                }
            }

            return default(T);
        }
    }
}
