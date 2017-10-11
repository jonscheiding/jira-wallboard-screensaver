using System;
using System.Windows.Forms;

namespace Jira.WallboardScreensaver.EditPreferences2 {
    public partial class JiraLoginForm : Form, IJiraLoginView {
        public JiraLoginForm() {
            InitializeComponent();
        }

        public string Username {
            get => usernameTextBox.Text;
            set => usernameTextBox.Text = value;
        }

        public string Password {
            get => passwordTextBox.Text;
            set => passwordTextBox.Text = value;
        }

        public event EventHandler LoginButtonClicked;
        public event EventHandler CancelButtonClicked;
        public event EventHandler ClearButtonClicked;

        private void OnCancelButtonClick(object sender, EventArgs e) {
            CancelButtonClicked?.Invoke(this, e);
        }

        private void OnClearButtonClick(object sender, EventArgs e) {
            ClearButtonClicked?.Invoke(this, e);
        }

        private void OnLoginButtonClick(object sender, EventArgs e) {
            LoginButtonClicked?.Invoke(this, e);
        }
    }
}
