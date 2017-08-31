using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jira.WallboardScreensaver.EditPreferences {
    public partial class EditPreferencesForm : Form, IEditPreferencesView {
        public EditPreferencesForm() {
            InitializeComponent();
        }

        public event EventHandler SaveButtonClicked;
        public event EventHandler CancelButtonClicked;

        public string DashboardUrl
        {
            get => dashboardUrlText.Text;
            set => dashboardUrlText.Text = value;
        }

        public string LoginCookies
        {
            get => loginCookiesText.Text;
            set => loginCookiesText.Text = value;
        }

        public void ShowError(string errorMessage)
        {
            MessageBox.Show(errorMessage, @"Invalid preferences", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void OnSaveButtonClicked(object sender, EventArgs e)
        {
            SaveButtonClicked?.Invoke(this, e);
        }

        private void OnCancelButtonClicked(object sender, EventArgs e)
        {
            CancelButtonClicked?.Invoke(this, e);
        }
    }
}
