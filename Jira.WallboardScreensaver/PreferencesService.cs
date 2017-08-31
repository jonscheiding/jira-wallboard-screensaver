﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Win32;

namespace Jira.WallboardScreensaver {
    public interface IPreferencesService {
        Preferences GetPreferences();
        void SetPreferences(Preferences preferences);
    }

    public class PreferencesService : IPreferencesService {
        public const string DashboardUriKey = "DashboardUri";
        public const string LoginCookiesKey = "LoginCookies";
        public const string CookieSeparator = @"=";
        private readonly RegistryKey _key;

        public PreferencesService(RegistryKey key) {
            _key = key;
        }

        public virtual Preferences GetPreferences() {
            return new Preferences {
                DashboardUri = ReadDashboardUri(_key),
                LoginCookies = ReadLoginCookies(_key)
            };
        }

        public void SetPreferences(Preferences preferences) {
            if (preferences.DashboardUri == null)
                _key.DeleteValue(DashboardUriKey, false);
            else
                _key.SetValue(DashboardUriKey, preferences.DashboardUri.ToString());

            _key.SetValue(LoginCookiesKey,
                preferences.LoginCookies.Select(kv => $@"{kv.Key}{CookieSeparator}{kv.Value}").ToArray());
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
            var loginCookies = (string[]) key.GetValue(LoginCookiesKey, new string[0]);
            if (loginCookies == null)
                return new Dictionary<string, string>();

            return loginCookies
                .Select(cookie => {
                    var parts = cookie.Split(new[] {CookieSeparator}, 2, StringSplitOptions.None);
                    if (parts.Length != 2)
                        throw new ArgumentException(
                            $@"Invalid cookie setting; expected a string of the form 'key{CookieSeparator}value'.",
                            LoginCookiesKey);
                    return parts;
                })
                .ToDictionary(kv => kv[0], kv => kv[1]);
        }
    }
}