using System;

namespace Jira.WallboardScreensaver.EditPreferences
{
    public interface IEditPreferencesView
    {
        event EventHandler SaveButtonClicked;
        event EventHandler CancelButtonClicked;
        string DashboardUrl { get; set; }
        string LoginCookies { get; set; }
        void Close();
    }
}