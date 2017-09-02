using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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

            view.Anonymous = preferences.LoginCookies.Count == 0;

            view.CancelButtonClicked += (o, args) => view.Close();
            view.SaveButtonClicked += async (o, args) => {
                if (await SavePreferences(view))
                    view.Close();
            };
        }

        private async Task<bool> SavePreferences(IEditPreferencesView view) {
            if (!ValidateDashboardUri(view, out Uri dashboardUri))
                return false;

            var preferences = new Preferences {
                DashboardUri = dashboardUri
            };

            if (!view.Anonymous) {
                if (string.IsNullOrEmpty(view.LoginUsername) || string.IsNullOrEmpty(view.LoginPassword)) {
                    view.ShowError("Please enter your username and password.");
                    return false;
                }

                view.Disabled = true;

                try {
                    preferences.LoginCookies = await _jira.Login(new Uri(dashboardUri, "/"),
                        view.LoginUsername,
                        view.LoginPassword);
                } catch (HttpRequestException x) {
                    view.ShowError(x.Message);
                    return false;
                } finally {
                    view.Disabled = false;
                }
            }

            _preferences.SetPreferences(preferences);

            return true;
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
    }
}