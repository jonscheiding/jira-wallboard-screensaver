using System;

namespace Jira.WallboardScreensaver.Screensaver
{
    public interface IScreensaverView
    {
        event EventHandler Load;
        event EventHandler Closed;
        void Close();
        void Show();
    }
}