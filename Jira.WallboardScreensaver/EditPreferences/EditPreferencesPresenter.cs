using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Jira.WallboardScreensaver.Services;

namespace Jira.WallboardScreensaver.EditPreferences {
    public class EditPreferencesPresenter : IPresenter<IEditPreferencesView> {
        class JiraLoginParent : IJiraLoginParent {
            private readonly Preferences _preferences;
            private readonly IEditPreferencesView _view;

            public JiraLoginParent(Preferences preferences, IEditPreferencesView view) {
                _preferences = preferences;
                _view = view;
            }

            public string JiraUrl => _preferences.JiraUri.ToString();
            public string Username => _preferences.LoginUsername;
            public void UpdateJiraCredentials(IReadOnlyDictionary<string, string> credentials, string username) {
                _preferences.LoginCookies = credentials;
                _preferences.LoginUsername = username;
                _view.DisplayHasCredentials = true;
                _view.DashboardItems = new IDashboardDisplayItem[0];
            }

            public void ClearJiraCredentials() {
                _preferences.LoginCookies = new Dictionary<string, string>();
                _preferences.LoginUsername = null;
                _view.DisplayHasCredentials = false;
                _view.DashboardItems = new IDashboardDisplayItem[0];
            }
        }

        private readonly IChildPresenter<IJiraLoginView, IJiraLoginParent> _childPresenter;
        private readonly IPreferencesService _preferences;
        private readonly IJiraService _jira;
        private readonly IErrorMessageService _errors;

        public EditPreferencesPresenter(IChildPresenter<IJiraLoginView, IJiraLoginParent> childPresenter,
            IPreferencesService preferences, IJiraService jira, IErrorMessageService errors) {
            _childPresenter = childPresenter;
            _preferences = preferences;
            _jira = jira;
            _errors = errors;
        }

        public void Initialize(IEditPreferencesView view) {
            var preferences = _preferences.GetPreferences();

            view.JiraUrl = preferences.JiraUri?.ToString();
            view.DisplayHasCredentials = preferences.LoginCookies.Count > 0;

            view.JiraLoginButtonClicked += (sender, e) => ShowJiraLoginView(view, preferences);
            view.SaveButtonClicked += (sender, e) => SavePreferences(view, preferences);
            view.CancelButtonClicked += (sender, e) => view.Close();
            view.LoadDashboardsButtonClicked += async (sender, e) => await LoadJiraDashboards(view, preferences);
        }

        private void ShowJiraLoginView(IEditPreferencesView view, Preferences preferences) {
            if (!TryValidateJiraUri(view, preferences)) {
                return;
            }

            var parent = new JiraLoginParent(preferences, view);

            var childView = view.CreateJiraLoginView();
            _childPresenter.Initialize(childView, parent);
            view.ShowJiraLoginView(childView);
        }

        private async Task LoadJiraDashboards(IEditPreferencesView view, Preferences preferences) {
            if (!TryValidateJiraUri(view, preferences)) {
                return;
            }

            IEnumerable<JiraDashboard> result;
            try {
                result = await _jira.GetDashboardsAsync(preferences.JiraUri, preferences.LoginCookies);
            } catch (Exception x) {
                _errors.ShowErrorMessage(view, x.Message, "Error Loading Dashboards");
                return;
            }

            view.DashboardItems = result.ToArray<IDashboardDisplayItem>();
        }

        private void SavePreferences(IEditPreferencesView view, Preferences preferences) {
            if (!TryValidateJiraUri(view, preferences)) {
                return;
            }

            if (view.SelectedDashboardItem == null) {
                _errors.ShowErrorMessage(view,
                    "Please select a dashboard.  To load your JIRA dashboards, click the \"Load dashboards\" button next to the JIRA URL.",
                    "Invalid Dashboard Selection");
                return;
            }

            preferences.DashboardId = ((JiraDashboard) view.SelectedDashboardItem).Id;

            _preferences.SetPreferences(preferences);
            view.Close();
        }

        private bool TryValidateJiraUri(IEditPreferencesView view, Preferences preferences) {
            try {
                preferences.JiraUri = new Uri(view.JiraUrl);
                return true;
            } catch (Exception x) {
                _errors.ShowErrorMessage(view, x.Message, "Invalid JIRA URL");
                return false;
            }
        }
    }
}