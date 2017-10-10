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
        private readonly IPreferencesService _preferences;
        private readonly IJiraService _jira;

        public EditPreferencesPresenter(IPreferencesService preferences, IJiraService jira) {
            _preferences = preferences;
            _jira = jira;
        }

        public void Initialize(IEditPreferencesView view) {
            var preferences = _preferences.GetPreferences();

            if (preferences.DashboardUri != null) {
                view.DashboardUrl = preferences.DashboardUri.ToString();
                view.JiraUrl = new Uri(preferences.DashboardUri, "/").ToString();
            }

            view.Anonymous = preferences.LoginCookies.Count == 0;
            view.LoginUsername = preferences.LoginUsername ?? string.Empty;

            view.CancelButtonClicked += (o, args) => view.Close();
            view.SaveButtonClicked += async (o, args) => {
                if (await SavePreferences(view))
                    view.Close();
            };
            view.LoadDashboardsButtonClicked += async (o, args) => {
                await LoadDashboardsIntoView(view);
            };
            view.SelectedDashboardItemChanged += (o, args) => RefreshDashboardUrl(view);
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

                preferences.LoginUsername = view.LoginUsername;

                view.Disabled = true;

                try {
                    preferences.LoginCookies = await _jira.LoginAsync(new Uri(dashboardUri, "/"),
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

        private async Task LoadDashboardsIntoView(IEditPreferencesView view) {
            if (!ValidateJiraUri(view, out Uri jiraUri))
                return;

            IReadOnlyDictionary<string, string> credentials = new Dictionary<string, string>();

            IEnumerable<JiraDashboard> dashboards;

            view.Disabled = false;
            try {
                if (!view.Anonymous) {
                    credentials = await _jira.LoginAsync(jiraUri, view.LoginUsername, view.LoginPassword);
                }

                dashboards = await _jira.GetDashboardsAsync(jiraUri, credentials);
            } catch (HttpRequestException x) {
                view.ShowError(x.Message);
                return;
            } finally {
                view.Disabled = false;
            }

            // ReSharper disable once CoVariantArrayConversion
            view.SetDashboardItems(dashboards.ToArray());
        }

        private void RefreshDashboardUrl(IEditPreferencesView view) {
            if (!ValidateJiraUri(view, out Uri jiraUri))
                return;

            var dashboardItem = (JiraDashboard) view.SelectedDashboardItem;

            var dashboardUri = new Uri(jiraUri, $"/plugins/servlet/Wallboard/?dashboardId={dashboardItem.Id}");
            view.DashboardUrl = dashboardUri.ToString();
        }

        private static bool ValidateJiraUri(IEditPreferencesView view, out Uri jiraUri) {
            try {
                jiraUri = new Uri(view.JiraUrl);
                return true;
            } catch (UriFormatException x) {
                view.ShowError(x.Message);
                jiraUri = null;
                return false;
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
    }
}