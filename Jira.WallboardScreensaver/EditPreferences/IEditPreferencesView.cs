using System;

namespace Jira.WallboardScreensaver.EditPreferences {
    public class LoginEventArgs : EventArgs {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public interface IEditPreferencesView {
        string DashboardUrl { get; set; }
        string LoginCookies { get; set; }
        bool Disabled { get; set; }
        event EventHandler SaveButtonClicked;
        event EventHandler CancelButtonClicked;
        event EventHandler<LoginEventArgs> LoginButtonClicked;
        void Close();

        void ShowError(string errorMessage);
    }
}