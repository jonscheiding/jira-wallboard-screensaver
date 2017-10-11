namespace Jira.WallboardScreensaver.EditPreferences {
    partial class EditPreferencesForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Button cancelButton;
            System.Windows.Forms.Button saveButton;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Button loadDashboardsButton;
            System.Windows.Forms.Label label2;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditPreferencesForm));
            this.jiraUrlTextBox = new System.Windows.Forms.TextBox();
            this.jiraLoginButton = new System.Windows.Forms.Button();
            this.dashboardsListBox = new System.Windows.Forms.ListBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            cancelButton = new System.Windows.Forms.Button();
            saveButton = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            loadDashboardsButton = new System.Windows.Forms.Button();
            label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Location = new System.Drawing.Point(290, 219);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(75, 23);
            cancelButton.TabIndex = 0;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += new System.EventHandler(this.OnCancelButtonClick);
            // 
            // saveButton
            // 
            saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            saveButton.Location = new System.Drawing.Point(209, 219);
            saveButton.Name = "saveButton";
            saveButton.Size = new System.Drawing.Size(75, 23);
            saveButton.TabIndex = 1;
            saveButton.Text = "Save";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += new System.EventHandler(this.OnSaveButtonClick);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 17);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(51, 13);
            label1.TabIndex = 2;
            label1.Text = "Jira URL:";
            // 
            // loadDashboardsButton
            // 
            loadDashboardsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            loadDashboardsButton.Image = global::Jira.WallboardScreensaver.Properties.Resources.ic_search;
            loadDashboardsButton.Location = new System.Drawing.Point(342, 12);
            loadDashboardsButton.Name = "loadDashboardsButton";
            loadDashboardsButton.Size = new System.Drawing.Size(23, 23);
            loadDashboardsButton.TabIndex = 5;
            this.toolTip1.SetToolTip(loadDashboardsButton, "Load dashboards");
            loadDashboardsButton.UseVisualStyleBackColor = true;
            loadDashboardsButton.Click += new System.EventHandler(this.OnLoadDashboardsButtonClick);
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(12, 42);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(67, 13);
            label2.TabIndex = 6;
            label2.Text = "Dashboards:";
            // 
            // jiraUrlTextBox
            // 
            this.jiraUrlTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.jiraUrlTextBox.Location = new System.Drawing.Point(96, 14);
            this.jiraUrlTextBox.Name = "jiraUrlTextBox";
            this.jiraUrlTextBox.Size = new System.Drawing.Size(211, 20);
            this.jiraUrlTextBox.TabIndex = 3;
            // 
            // jiraLoginButton
            // 
            this.jiraLoginButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.jiraLoginButton.Image = global::Jira.WallboardScreensaver.Properties.Resources.ic_person_outline;
            this.jiraLoginButton.Location = new System.Drawing.Point(313, 12);
            this.jiraLoginButton.Name = "jiraLoginButton";
            this.jiraLoginButton.Size = new System.Drawing.Size(23, 23);
            this.jiraLoginButton.TabIndex = 4;
            this.toolTip1.SetToolTip(this.jiraLoginButton, "Log in to JIRA");
            this.jiraLoginButton.UseVisualStyleBackColor = true;
            this.jiraLoginButton.Click += new System.EventHandler(this.OnJiraLoginButtonClick);
            // 
            // dashboardsListBox
            // 
            this.dashboardsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dashboardsListBox.FormattingEnabled = true;
            this.dashboardsListBox.Location = new System.Drawing.Point(96, 40);
            this.dashboardsListBox.Name = "dashboardsListBox";
            this.dashboardsListBox.Size = new System.Drawing.Size(269, 173);
            this.dashboardsListBox.TabIndex = 7;
            // 
            // EditPreferencesForm
            // 
            this.AcceptButton = saveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = cancelButton;
            this.ClientSize = new System.Drawing.Size(377, 254);
            this.Controls.Add(this.dashboardsListBox);
            this.Controls.Add(label2);
            this.Controls.Add(loadDashboardsButton);
            this.Controls.Add(this.jiraLoginButton);
            this.Controls.Add(this.jiraUrlTextBox);
            this.Controls.Add(label1);
            this.Controls.Add(saveButton);
            this.Controls.Add(cancelButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditPreferencesForm";
            this.Text = "EditPreferencesForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox jiraUrlTextBox;
        private System.Windows.Forms.Button jiraLoginButton;
        private System.Windows.Forms.ListBox dashboardsListBox;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}