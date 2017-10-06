using System;
using System.Collections.Generic;

namespace Jira.WallboardScreensaver.EditPreferences {
    public interface IDashboardDisplayItem {
        string ToString();
    }

    public interface IEditPreferencesView {
        string DashboardUrl { get; set; }
        string JiraUrl { get; set; }
        string LoginUsername { get; set; }
        string LoginPassword { get; set; }
        bool Anonymous { get; set; }
        bool Disabled { get; set; }
        IDashboardDisplayItem SelectedDashboardItem { get; set; }

        event EventHandler SaveButtonClicked;
        event EventHandler CancelButtonClicked;
        event EventHandler LoadDashboardsButtonClicked;
        event EventHandler SelectedDashboardItemChanged;

        void SetDashboardItems(IDashboardDisplayItem[] items);
        void Close();
        void ShowError(string errorMessage);
    }
}