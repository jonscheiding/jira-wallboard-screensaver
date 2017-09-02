using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using Microsoft.Win32;

namespace Jira.WallboardScreensaver.Services {
    public interface IPreferencesService {
        Preferences GetPreferences();
        void SetPreferences(Preferences preferences);
    }

    public class PreferencesService : IPreferencesService {
        public const string DashboardUriKey = "DashboardUri";
        public const string LoginCookiesKey = "LoginCookies";
        public const string LoginUsernameKey = "LoginUsername";

        private static readonly JavaScriptSerializer Serializer = new JavaScriptSerializer();

        private readonly RegistryKey _key;

        public PreferencesService(RegistryKey key) {
            _key = key;
        }

        public virtual Preferences GetPreferences() {
            return new Preferences {
                DashboardUri = ReadDashboardUri(_key),
                LoginCookies = ReadLoginCookies(_key),
                LoginUsername = (string)_key.GetValue(LoginUsernameKey)
            };
        }

        public void SetPreferences(Preferences preferences) {
            if (preferences.DashboardUri == null)
                _key.DeleteValue(DashboardUriKey, false);
            else
                _key.SetValue(DashboardUriKey, preferences.DashboardUri.ToString());

            _key.SetValue(LoginCookiesKey, Serializer.Serialize(preferences.LoginCookies));
            if (preferences.LoginUsername == null) {
                _key.DeleteValue(LoginUsernameKey, false);
            } else {
                _key.SetValue(LoginUsernameKey, preferences.LoginUsername);
            }
        }

        private static Uri ReadDashboardUri(RegistryKey key) {
            var dashboardUri = (string) key.GetValue(DashboardUriKey, null);
            if (dashboardUri == null)
                return null;

            try {
                return new Uri(dashboardUri);
            }
            catch (UriFormatException x) {
                throw new ArgumentException(x.Message, DashboardUriKey, x);
            }
        }

        private static Dictionary<string, string> ReadLoginCookies(RegistryKey key) {
            var loginCookies = (string) key.GetValue(LoginCookiesKey, null);
            if (loginCookies == null)
                return new Dictionary<string, string>();

            return Serializer.Deserialize<Dictionary<string, string>>(loginCookies);
        }
    }
}