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
        public const string JiraUriKey = "JiraUri";
        public const string LoginCookiesKey = "LoginCookies";
        public const string LoginUsernameKey = "LoginUsername";
        public const string DashboardIdKey = "DashboardId";

        private static readonly JavaScriptSerializer Serializer = new JavaScriptSerializer();

        private readonly RegistryKey _key;

        public PreferencesService(RegistryKey key) {
            _key = key;
        }

        public virtual Preferences GetPreferences() {
            return new Preferences {
                JiraUri = ReadJiraUri(_key),
                DashboardId = ReadDashboardId(_key),
                LoginCookies = ReadLoginCookies(_key),
                LoginUsername = (string)_key.GetValue(LoginUsernameKey)
            };
        }

        public void SetPreferences(Preferences preferences) {
            if (preferences.JiraUri == null)
                _key.DeleteValue(JiraUriKey, false);
            else
                _key.SetValue(JiraUriKey, preferences.JiraUri.ToString());

            if (preferences.DashboardId == null)
                _key.DeleteValue(DashboardIdKey, false);
            else
                _key.SetValue(DashboardIdKey, preferences.DashboardId.Value);

            if (preferences.LoginUsername == null) {
                _key.DeleteValue(LoginUsernameKey, false);
            } else {
                _key.SetValue(LoginUsernameKey, preferences.LoginUsername);
            }

            _key.SetValue(LoginCookiesKey, Serializer.Serialize(preferences.LoginCookies));
        }

        private static Uri ReadJiraUri(RegistryKey key) {
            var jiraUri = (string)key.GetValue(JiraUriKey, null);
            if (jiraUri == null)
                return null;

            try {
                return new Uri(jiraUri);
            } catch (UriFormatException x) {
                throw new ArgumentException(x.Message, JiraUriKey, x);
            }
        }

        private static int? ReadDashboardId(RegistryKey key) {
            var dashboardId = key.GetValue(DashboardIdKey, null);

            return (int?) dashboardId;
        }

        private static Dictionary<string, string> ReadLoginCookies(RegistryKey key) {
            var loginCookies = (string) key.GetValue(LoginCookiesKey, null);
            if (loginCookies == null)
                return new Dictionary<string, string>();

            return Serializer.Deserialize<Dictionary<string, string>>(loginCookies);
        }
    }
}