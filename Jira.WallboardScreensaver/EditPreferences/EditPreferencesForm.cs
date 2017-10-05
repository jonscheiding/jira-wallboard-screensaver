using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Jira.WallboardScreensaver.EditPreferences { 
    public partial class EditPreferencesForm : Form, IEditPreferencesView {
        private class DashboardDisplayItemWrapper {
            public IDashboardDisplayItem Item { get; }

            public DashboardDisplayItemWrapper(IDashboardDisplayItem item) {
                Item = item;
            }

            public override string ToString() {
                return Item.Label;
            }
        }

        public EditPreferencesForm() {
            InitializeComponent();
        }

        public event EventHandler SaveButtonClicked;
        public event EventHandler CancelButtonClicked;
        public event EventHandler LoadDashboardsButtonClicked;
        public event EventHandler SelectedDashboardChanged;

        public string DashboardUrl {
            get => dashboardUrlText.Text;
            set => dashboardUrlText.Text = value;
        }

        public string JiraUrl {
            get => jiraUrlText.Text;
            set => jiraUrlText.Text = value;
        }

        public string LoginUsername {
            get => loginUsernameText.Text;
            set => loginUsernameText.Text = value;
        }

        public string LoginPassword {
            get => loginPasswordText.Text;
            set => loginPasswordText.Text = value;
        }

        public bool Anonymous {
            get => anonymousCheckbox.Checked;
            set => anonymousCheckbox.Checked = value;
        }

        public IDashboardDisplayItem SelectedDashboardItem {
            get => ((DashboardDisplayItemWrapper) dashboardsListBox.SelectedItem)?.Item;
            set {
                var index = dashboardsListBox.Items.IndexOf(value);
                if (index == -1) {
                    throw new ArgumentException("Provided value is not in the list.");
                }
                dashboardsListBox.SelectedIndex = index;
            }
        }

        public bool Disabled {
            get => !Enabled;
            set => Enabled = !value;
        }

        public void SetDashboardItems(IEnumerable<IDashboardDisplayItem> dashboardItems) {
            dashboardsListBox.SelectedIndex = -1;
            dashboardsListBox.Items.Clear();
            dashboardsListBox.Items.AddRange(
                dashboardItems.Select(item => new DashboardDisplayItemWrapper(item)).ToArray<object>());
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

        private void OnLoadDashboardsButtonClicked(object sender, EventArgs e) {
            LoadDashboardsButtonClicked?.Invoke(this, e);
        }

        private void OnAnonymousCheckboxChanged(object sender, EventArgs e) {
            loginPasswordText.Enabled = loginUsernameText.Enabled = !((CheckBox) sender).Checked;
        }

        private void OnDashboardsListBoxSelectedIndexChanged(object sender, EventArgs e) {
            SelectedDashboardChanged?.Invoke(this, e);
        }
    }
}