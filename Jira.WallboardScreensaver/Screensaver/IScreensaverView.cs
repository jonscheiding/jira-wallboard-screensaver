using System;

namespace Jira.WallboardScreensaver.Screensaver {
    public interface IScreensaverView {
        bool NavigationInProgress { get; }
        bool ControlsVisible { get; set; }
        event EventHandler Load;
        event EventHandler Closed;
        event EventHandler ExitButtonClicked;

        void Close();
        void Navigate(Uri uri);
    }
}