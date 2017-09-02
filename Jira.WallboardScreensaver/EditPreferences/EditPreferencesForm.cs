using System;
using System.Windows.Forms;

namespace Jira.WallboardScreensaver.EditPreferences { 
    public partial class EditPreferencesForm : Form, IEditPreferencesView {
        public EditPreferencesForm() {
            InitializeComponent();
        }

        public event EventHandler SaveButtonClicked;
        public event EventHandler CancelButtonClicked;

        public string DashboardUrl {
            get => dashboardUrlText.Text;
            set => dashboardUrlText.Text = value;
        }

        public string LoginUsername {
            get => loginUsernameText.Text;
            set => loginUsernameText.Text = value;
        }

        public string LoginPassword {
            get => loginPasswordText.Text;
            set => loginPasswordText.Text = value;
        }

        public bool Disabled {
            get => !Enabled;
            set => Enabled = !value;
        }

        public void ShowError(string errorMessage) {
            MessageBox.Show(errorMessage, @"Invalid preferences", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void OnSaveButtonClicked(object sender, EventArgs e) {
            SaveButtonClicked?.Invoke(this, e);
        }

        private void OnCancelButtonClicked(object sender, EventArgs e) {
            CancelButtonClicked?.Invoke(this, e);
        }
    }
}