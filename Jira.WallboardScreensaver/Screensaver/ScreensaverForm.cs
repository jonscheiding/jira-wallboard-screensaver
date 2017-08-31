using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jira.WallboardScreensaver.Screensaver {
    public partial class ScreensaverForm : Form, IScreensaverView {
        private const int NavigationEndDelay = 250;

        public ScreensaverForm() {
            InitializeComponent();
            Cursor.Hide();
        }

        public void Navigate(Uri uri) {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));

            if (webBrowser.InvokeRequired) webBrowser.Invoke((Action) (() => Navigate(uri)));
            else webBrowser.Navigate(uri.ToString());
        }

        public bool ControlsVisible {
            get { return exitButton.Visible; }
            set {
                if (InvokeRequired) {
                    Invoke((Action) (() => ControlsVisible = value));
                }
                else {
                    ToggleCursorVisible(value, exitButton.Visible);
                    exitButton.Visible = value;
                }
            }
        }

        public event EventHandler ExitButtonClicked;

        public bool NavigationInProgress { get; private set; }

        private void OnWebBrowserNavigated(object sender, WebBrowserNavigatedEventArgs e) {
            //
            // Need a small delay or else navigation sometimes triggers user activity
            //
            Task.Delay(NavigationEndDelay).ContinueWith(t => NavigationInProgress = false);
        }

        private void OnWebBrowserNavigating(object sender, WebBrowserNavigatingEventArgs e) {
            NavigationInProgress = true;
        }

        private void OnExitButtonClick(object sender, EventArgs e) {
            ExitButtonClicked?.Invoke(this, e);
        }

        private void ToggleCursorVisible(bool visible, bool wasPreviouslyVisible) {
            if (visible == wasPreviouslyVisible)
                return;

            if (visible)
                Cursor.Show();
            else
                Cursor.Hide();
        }
    }
}