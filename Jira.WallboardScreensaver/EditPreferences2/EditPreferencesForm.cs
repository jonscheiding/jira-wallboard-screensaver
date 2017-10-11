using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jira.WallboardScreensaver.EditPreferences2 {
    public partial class EditPreferencesForm : Form, IEditPreferencesView {
        public EditPreferencesForm() {
            InitializeComponent();
        }

        public string JiraUrl {
            get => jiraUrlTextBox.Text;
            set => jiraUrlTextBox.Text = value;
        }

        public IDashboardDisplayItem[] DashboardItems {
            get => dashboardsListBox.Items.Cast<IDashboardDisplayItem>().ToArray();
            set {
                dashboardsListBox.Items.Clear();
                dashboardsListBox.Items.AddRange(value.ToArray<object>());
            }
        }

        public IDashboardDisplayItem SelectedDashboardItem {
            get => (IDashboardDisplayItem) dashboardsListBox.SelectedItem;
            set => dashboardsListBox.SelectedItem = value;
        }

        public event EventHandler SelectedDashboardItemChanged;
        public event EventHandler JiraLoginButtonClicked;
        public event EventHandler LoadDashboardsButtonClicked;
        public event EventHandler SaveButtonClicked;
        public event EventHandler CancelButtonClicked;

        public IJiraLoginView CreateJiraLoginView() {
            return new JiraLoginForm();
        }

        private void OnJiraLoginButtonClick(object sender, EventArgs e) {
            JiraLoginButtonClicked?.Invoke(this, e);
        }

        private void OnLoadDashboardsButtonClick(object sender, EventArgs e) {
            LoadDashboardsButtonClicked?.Invoke(this, e);
        }

        private void OnSaveButtonClick(object sender, EventArgs e) {
            SaveButtonClicked?.Invoke(this, e);
        }

        private void OnCancelButtonClick(object sender, EventArgs e) {
            CancelButtonClicked?.Invoke(this, e);
        }
    }
}
