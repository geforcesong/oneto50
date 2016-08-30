using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO.IsolatedStorage;

namespace OneTo50.Utility
{
    public class IsolatedStorageManager
    {
        public static void Save(string key, object val)
        {
            if (string.IsNullOrEmpty(key))
                return;
            if (IsolatedStorageSettings.ApplicationSettings.Contains(key))
                IsolatedStorageSettings.ApplicationSettings[key] = val;
            else
                IsolatedStorageSettings.ApplicationSettings.Add(key, val);
        }

        public static void Delete(string key)
        {
            if (string.IsNullOrEmpty(key))
                return;
            if (IsolatedStorageSettings.ApplicationSettings.Contains(key))
                IsolatedStorageSettings.ApplicationSettings.Remove(key);
        }

        public static bool Contains(string key)
        {
            return IsolatedStorageSettings.ApplicationSettings.Contains(key);
        }

        public static string GetStringValue(string key)
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains(key) && IsolatedStorageSettings.ApplicationSettings[key] != null)
                return IsolatedStorageSettings.ApplicationSettings[key].ToString();
            else
                return "";
        }

        public static int GetIntValue(string key)
        {
            int r;
            if (!IsolatedStorageSettings.ApplicationSettings.Contains(key))
                return 0;
            else
            {
                int.TryParse(IsolatedStorageSettings.ApplicationSettings[key].ToString(), out r);
                return r;
            }
        }

        public static bool GetBooleanValue(string key)
        {
            if (!IsolatedStorageSettings.ApplicationSettings.Contains(key))
                return false;
            else
            {
                bool r;
                bool.TryParse(IsolatedStorageSettings.ApplicationSettings[key].ToString(), out r);
                return r;
            }
        }

        public static object GetObjectValue(string key)
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains(key) && IsolatedStorageSettings.ApplicationSettings[key] != null)
                return IsolatedStorageSettings.ApplicationSettings[key];
            else
                return null;
        }
    }
}
