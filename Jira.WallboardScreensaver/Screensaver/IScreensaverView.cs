using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jira.WallboardScreensaver.Screensaver
{
    public interface IScreensaverView
    {
        event EventHandler Load;
        event EventHandler Closed;

        bool NavigationInProgress { get; }
        void Close();
        void Show();
        void Navigate(string url);
    }
}