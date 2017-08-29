using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jira.WallboardScreensaver.Screensaver
{
    public interface IScreensaverView
    {
        event EventHandler Load;
        event EventHandler Closed;
        event EventHandler ExitButtonClicked;

        bool NavigationInProgress { get; }
        bool ControlsVisible { get; set; }

        void Close();
        void Navigate(Uri uri);
    }
}