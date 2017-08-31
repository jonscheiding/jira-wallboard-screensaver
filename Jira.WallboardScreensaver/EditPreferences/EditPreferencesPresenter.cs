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
                if (SavePreferences(view))
                {
                    view.Close();
                }
            };
        }

        private bool SavePreferences(IEditPreferencesView view)
        {
            Uri dashboardUri;
            Dictionary<string, string> loginCookies;

            if (!ValidateDashboardUri(view, out dashboardUri) ||
                !ValidateLoginCookies(view, out loginCookies))
            {
                return false;
            }

            _preferences.SetPreferences(new Preferences
            {
                DashboardUri = dashboardUri,
                LoginCookies = loginCookies
            });

            return true;
        }

        private bool ValidateDashboardUri(IEditPreferencesView view, out Uri dashboardUri)
        {
            try
            {
                dashboardUri = new Uri(view.DashboardUrl);
                return true;
            }
            catch (UriFormatException x)
            {
                view.ShowError(x.Message);
                dashboardUri = null;
                return false;
            }
        }

        private bool ValidateLoginCookies(IEditPreferencesView view, out Dictionary<string, string> loginCookies)
        {
            if (string.IsNullOrEmpty(view.LoginCookies))
            {
                loginCookies = new Dictionary<string, string>();
                return true;
            }

            try
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

                return true;
            }
            catch (ArgumentException)
            {
                view.ShowError("Invalid cookies.  Cookies must be in the form 'cookie1=value1;cookie2=value2'.");
                loginCookies = null;
                return false;
            }
        }
    }
}
