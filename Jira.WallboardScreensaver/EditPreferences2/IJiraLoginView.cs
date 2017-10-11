using System;
using System.Collections.Generic;

namespace Jira.WallboardScreensaver.EditPreferences2 {
    public interface IJiraLoginView {
        string Username { get; set; }
        string Password { get; set; }
        bool Disabled { get; set; }

        void Close();

        event EventHandler LoginButtonClicked;
        event EventHandler CancelButtonClicked;
        event EventHandler ClearButtonClicked;
    }

    public interface IJiraLoginParent {
        string JiraUrl { get; }
        string Username { get; set; }

        void UpdateJiraCredentials(IReadOnlyDictionary<string, string> credentials);
        void ClearJiraCredentials();
    }
}