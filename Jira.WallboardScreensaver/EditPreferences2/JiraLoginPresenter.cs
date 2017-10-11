using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jira.WallboardScreensaver.Services;

namespace Jira.WallboardScreensaver.EditPreferences2 {
    public class JiraLoginPresenter : IChildPresenter<IJiraLoginView, IJiraLoginParent> {
        private readonly IJiraService _jiraService;
        public JiraLoginPresenter(IJiraService jiraService) {
            _jiraService = jiraService;
        }

        public void Initialize(IJiraLoginView view, IJiraLoginParent parent) {
            view.Username = parent.Username;

            view.LoginButtonClicked += (sender, e) => OnLoginButtonClicked(view, parent);
            view.CancelButtonClicked += (sender, e) => view.Close();
            view.ClearButtonClicked += (sender, e) => {
                parent.ClearJiraCredentials();
                view.Close();
            };
        }

        private async void OnLoginButtonClicked(IJiraLoginView view, IJiraLoginParent parent) {
            if (string.IsNullOrEmpty(view.Username) || string.IsNullOrEmpty(view.Password)) {
                view.ShowError("Please enter your username and password.");
                return;
            }
            
            try {
                var credentials = await _jiraService.LoginAsync(
                    new Uri(parent.JiraUrl),
                    view.Username, view.Password);

                parent.UpdateJiraCredentials(credentials);

                view.Close();
            } catch (Exception x) {
                view.ShowError(x.Message);
            }
        }
    }
}