using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jira.WallboardScreensaver.EditPreferences {
    public class EditPreferencesPresenter : IPresenter<IEditPreferencesView> {
        private readonly IPreferencesService _preferences;

        public EditPreferencesPresenter(IPreferencesService preferences)
        {
            _preferences = preferences;
        }

        public void Initialize(IEditPreferencesView view)
        {
            var preferences = _preferences.GetPreferences();

            if (preferences.DashboardUri != null)
            {
                view.DashboardUrl = preferences.DashboardUri.ToString();
            }

            view.LoginCookies = string.Join(";", preferences.LoginCookies.Select(kv => $@"{kv.Key}={kv.Value}"));

            view.CancelButtonClicked += (o, args) => view.Close();
            view.SaveButtonClicked += (o, args) =>
            {
                SavePreferences(view);
                view.Close();
            };
        }

        private void SavePreferences(IEditPreferencesView view)
        {
            var loginCookies = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(view.LoginCookies))
            {
                loginCookies = view.LoginCookies.Split(';')
                    .Select(c =>
                    {
                        var parts = c.Split(new[] {'='}, 2);
                        if (parts.Length != 2)
                        {
                            throw new ArgumentException($@"Invalid dictionary argument '{c}'.");
                        }
                        return parts;
                    })
                    .ToDictionary(kv => kv[0], kv => kv[1]);
            }

            var preferences = new Preferences
            {
                DashboardUri = new Uri(view.DashboardUrl),
                LoginCookies = loginCookies
            };

            _preferences.SetPreferences(preferences);
        }
    }
}
