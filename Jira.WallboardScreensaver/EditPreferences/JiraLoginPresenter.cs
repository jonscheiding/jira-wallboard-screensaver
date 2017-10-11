using System;
using Jira.WallboardScreensaver.Services;

namespace Jira.WallboardScreensaver.EditPreferences {
    public class JiraLoginPresenter : IChildPresenter<IJiraLoginView, IJiraLoginParent> {
        private readonly IJiraService _jiraService;
        private readonly IErrorMessageService _errors;

        public JiraLoginPresenter(IJiraService jiraService, IErrorMessageService errors) {
            _jiraService = jiraService;
            _errors = errors;
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
                _errors.ShowErrorMessage(view, "Please enter your username and password.", "Invalid Credentials");
                return;
            }

            view.Disabled = true;

            try {
                var credentials = await _jiraService.LoginAsync(
                    new Uri(parent.JiraUrl),
                    view.Username, view.Password);

                parent.UpdateJiraCredentials(credentials);

                view.Close();
            } catch (Exception x) {
                _errors.ShowErrorMessage(view, x.Message, "Invalid Credentials");
            } finally {
                view.Disabled = false;
            }
        }
    }
}