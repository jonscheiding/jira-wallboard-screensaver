using System;

namespace Jira.WallboardScreensaver.EditPreferences2 {
    public interface IJiraLoginView {
        string Username { get; set; }
        string Password { get; set; }

        void Close();

        event EventHandler LoginButtonClicked;
        event EventHandler CancelButtonClicked;
        event EventHandler ClearButtonClicked;
    }
}