using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jira.WallboardScreensaver.Screensaver {
    public partial class ScreensaverForm : Form, IScreensaverView
    {
        public ScreensaverForm()
        {
            InitializeComponent();
        }

        public void Navigate(string url) {
            if (webBrowser.InvokeRequired) {
                webBrowser.Invoke((Action)(() => Navigate(url)));
            } else {
                webBrowser.Navigate(url);
            }
        }

        public event WebBrowserNavigatedEventHandler Navigated
        {
            add { webBrowser.Navigated += value; }
            remove { webBrowser.Navigated -= value; }
        }

        public event WebBrowserNavigatingEventHandler Navigating
        {
            add { webBrowser.Navigating += value; }
            remove { webBrowser.Navigating -= value; }
        }
    }
}
