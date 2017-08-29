using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jira.WallboardScreensaver.Screensaver {
    public partial class ScreensaverForm : Form, IScreensaverView
    {
        private const int NAVIGATION_DELAY_MS = 250;

        public ScreensaverForm()
        {
            InitializeComponent();
        }

        public Task NavigateToAsync(string url)
        {
            return Task.Run(() => NavigateTo(url))
                //
                // We need a slight delay here to prevent the navigation from triggering user events
                //
                .ContinueWith(t => Task.Delay(NAVIGATION_DELAY_MS)).Unwrap();
        }

        private void NavigateTo(string url) {
            if (webBrowser.InvokeRequired) {
                webBrowser.Invoke((Action)(() => NavigateTo(url)));
            } else {
                webBrowser.Navigate(url);
            }
        }
    }
}
