using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Script.Serialization;
using Jira.WallboardScreensaver.Services;

namespace Jira.WallboardScreensaver.EditPreferences {
    public class EditPreferencesPresenter : IPresenter<IEditPreferencesView> {
        private static readonly JavaScriptSerializer Serializer = new JavaScriptSerializer();

        private readonly IPreferencesService _preferences;
        private readonly IJiraService _jira;

        public EditPreferencesPresenter(IPreferencesService preferences, IJiraService jira) {
            _preferences = preferences;
            _jira = jira;
        }

        public void Initialize(IEditPreferencesView view) {
            var preferences = _preferences.GetPreferences();

            if (preferences.DashboardUri != null)
                view.DashboardUrl = preferences.DashboardUri.ToString();

            view.LoginCookies = Serializer.Serialize(preferences.LoginCookies);

            view.CancelButtonClicked += (o, args) => view.Close();
            view.SaveButtonClicked += (o, args) => {
                if (SavePreferences(view))
                    view.Close();
            };
            view.LoginButtonClicked += (o, args) => LoginToJira(view, args);
        }

        private bool SavePreferences(IEditPreferencesView view) {
            if (!ValidateDashboardUri(view, out Uri dashboardUri) ||
                !ValidateLoginCookies(view, out Dictionary<string, string> loginCookies))
                return false;

            _preferences.SetPreferences(new Preferences {
                DashboardUri = dashboardUri,
                LoginCookies = loginCookies
            });

            return true;
        }

        private async void LoginToJira(IEditPreferencesView view, LoginEventArgs args) {
            if (!ValidateDashboardUri(view, out Uri dashboardUri)) {
                return;
            }

            var baseUri = new Uri(dashboardUri, "/");

            try {
                var result = await _jira.Login(baseUri, args.Username, args.Password);
                view.LoginCookies = Serializer.Serialize(result);
            } catch (HttpRequestException) {
                view.ShowError("The login failed.");
            }
        }


        private static bool ValidateDashboardUri(IEditPreferencesView view, out Uri dashboardUri) {
            try {
                dashboardUri = new Uri(view.DashboardUrl);
                return true;
            }
            catch (UriFormatException x) {
                view.ShowError(x.Message);
                dashboardUri = null;
                return false;
            }
        }

        private static bool ValidateLoginCookies(IEditPreferencesView view, out Dictionary<string, string> loginCookies) {
            if (string.IsNullOrEmpty(view.LoginCookies)) {
                loginCookies = new Dictionary<string, string>();
                return true;
            }

            try {
                loginCookies = Serializer.Deserialize<Dictionary<string, string>>(view.LoginCookies);

                return true;
            }
            catch (ArgumentException) {
                view.ShowError("Invalid cookies.  Cookies must be in JSON format.");
                loginCookies = null;
                return false;
            }
        }
    }
}