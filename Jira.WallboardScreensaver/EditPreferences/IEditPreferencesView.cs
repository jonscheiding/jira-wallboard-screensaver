using System;

namespace Jira.WallboardScreensaver.EditPreferences {
    public interface IEditPreferencesView {
        string JiraUrl { get; set; }
        IDashboardDisplayItem[] DashboardItems { get; set; }
        IDashboardDisplayItem SelectedDashboardItem { get; set; }

        event EventHandler SelectedDashboardItemChanged;
        event EventHandler JiraLoginButtonClicked;
        event EventHandler LoadDashboardsButtonClicked;
        event EventHandler SaveButtonClicked;
        event EventHandler CancelButtonClicked;

        IJiraLoginView CreateJiraLoginView();
        void Close();
    }

    public interface IDashboardDisplayItem {
        string ToString();
    }
}