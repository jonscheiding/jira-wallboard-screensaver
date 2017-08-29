using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jira.WallboardScreensaver.Screensaver
{
    public interface IScreensaverView
    {
        event EventHandler Load;
        event EventHandler Closed;
        event WebBrowserNavigatedEventHandler Navigated;
        event WebBrowserNavigatingEventHandler Navigating;

        void Close();
        void Show();
        void Navigate(string url);
    }
}