using System;

namespace Jira.WallboardScreensaver.EditPreferences {
     public interface IEditPreferencesView {
        string DashboardUrl { get; set; }
        string LoginUsername { get; set; }
        string LoginPassword { get; set; }
        bool Disabled { get; set; }

        event EventHandler SaveButtonClicked;
        event EventHandler CancelButtonClicked;

        void Close();
        void ShowError(string errorMessage);
    }
}