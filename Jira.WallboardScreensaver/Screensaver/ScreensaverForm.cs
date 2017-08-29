using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jira.WallboardScreensaver.Screensaver {
    public partial class ScreensaverForm : Form, IScreensaverView
    {
        private const int NAVIGATION_END_DELAY = 250;

        public ScreensaverForm()
        {
            InitializeComponent();
        }

        public void Navigate(Uri uri) {
            if (uri == null)
            {
                throw new ArgumentNullException("uri");
            }

            if (webBrowser.InvokeRequired) {
                webBrowser.Invoke((Action)(() => Navigate(uri)));
            } else {
                webBrowser.Navigate(uri.ToString());
            }
        }

        public bool NavigationInProgress { get; private set; }

        private void OnWebBrowserNavigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            //
            // Need a small delay or else navigation sometimes triggers user activity
            //
            Task.Delay(NAVIGATION_END_DELAY).ContinueWith(t => NavigationInProgress = false);
        }

        private void OnWebBrowserNavigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            NavigationInProgress = true;
        }
    }
}
