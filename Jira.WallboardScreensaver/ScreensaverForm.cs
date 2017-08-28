using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jira.WallboardScreensaver {
    public partial class ScreensaverForm : Form
    {
        private bool _ignoreUserActivity;

        public ScreensaverForm(UserActivityFilter filter)
        {
            filter.UserActivity += OnUserActivity;
            Disposed += (obj, e) => filter.UserActivity -= OnUserActivity;

            InitializeComponent();
        }

        private void OnUserActivity()
        {
            if (_ignoreUserActivity)
            {
                return;
            }

            Close();
        }

        private void OnLoad(object sender, EventArgs e) {
            _ignoreUserActivity = true;
            Task.Delay(1000).ContinueWith(t => _ignoreUserActivity = false);
        }
    }
}
