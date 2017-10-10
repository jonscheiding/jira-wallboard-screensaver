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
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Button cancelButton;
            System.Windows.Forms.Button saveButton;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label6;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditPreferencesForm));
            this.dashboardUrlText = new System.Windows.Forms.TextBox();
            this.loginUsernameText = new System.Windows.Forms.TextBox();
            this.loginPasswordText = new System.Windows.Forms.TextBox();
            this.anonymousCheckbox = new System.Windows.Forms.CheckBox();
            this.jiraUrlText = new System.Windows.Forms.TextBox();
            this.dashboardsListBox = new System.Windows.Forms.ListBox();
            this.loadDashboardsButton = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            cancelButton = new System.Windows.Forms.Button();
            saveButton = new System.Windows.Forms.Button();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(13, 317);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(87, 13);
            label1.TabIndex = 0;
            label1.Text = "Dashboard URL:";
            // 
            // cancelButton
            // 
            cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Location = new System.Drawing.Point(309, 360);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(75, 23);
            cancelButton.TabIndex = 4;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += new System.EventHandler(this.OnCancelButtonClicked);
            // 
            // saveButton
            // 
            saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            saveButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            saveButton.Location = new System.Drawing.Point(228, 360);
            saveButton.Name = "saveButton";
            saveButton.Size = new System.Drawing.Size(75, 23);
            saveButton.TabIndex = 5;
            saveButton.Text = "OK";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += new System.EventHandler(this.OnSaveButtonClicked);
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(12, 41);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(58, 13);
            label3.TabIndex = 6;
            label3.Text = "Username:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(12, 66);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(56, 13);
            label4.TabIndex = 8;
            label4.Text = "Password:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(12, 90);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(65, 13);
            label2.TabIndex = 10;
            label2.Text = "Anonymous:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(13, 118);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(64, 13);
            label5.TabIndex = 13;
            label5.Text = "Dashboards";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(12, 15);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(58, 13);
            label6.TabIndex = 16;
            label6.Text = "JIRA URL:";
            // 
            // dashboardUrlText
            // 
            this.dashboardUrlText.Location = new System.Drawing.Point(105, 314);
            this.dashboardUrlText.Multiline = true;
            this.dashboardUrlText.Name = "dashboardUrlText";
            this.dashboardUrlText.ReadOnly = true;
            this.dashboardUrlText.Size = new System.Drawing.Size(279, 36);
            this.dashboardUrlText.TabIndex = 3;
            // 
            // loginUsernameText
            // 
            this.loginUsernameText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loginUsernameText.Location = new System.Drawing.Point(105, 38);
            this.loginUsernameText.Name = "loginUsernameText";
            this.loginUsernameText.Size = new System.Drawing.Size(279, 20);
            this.loginUsernameText.TabIndex = 7;
            // 
            // loginPasswordText
            // 
            this.loginPasswordText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loginPasswordText.Location = new System.Drawing.Point(105, 63);
            this.loginPasswordText.Name = "loginPasswordText";
            this.loginPasswordText.PasswordChar = '●';
            this.loginPasswordText.Size = new System.Drawing.Size(279, 20);
            this.loginPasswordText.TabIndex = 9;
            // 
            // anonymousCheckbox
            // 
            this.anonymousCheckbox.AutoSize = true;
            this.anonymousCheckbox.Location = new System.Drawing.Point(105, 90);
            this.anonymousCheckbox.Margin = new System.Windows.Forms.Padding(2);
            this.anonymousCheckbox.Name = "anonymousCheckbox";
            this.anonymousCheckbox.Size = new System.Drawing.Size(15, 14);
            this.anonymousCheckbox.TabIndex = 11;
            this.anonymousCheckbox.UseVisualStyleBackColor = true;
            this.anonymousCheckbox.CheckedChanged += new System.EventHandler(this.OnAnonymousCheckboxChanged);
            // 
            // jiraUrlText
            // 
            this.jiraUrlText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.jiraUrlText.Location = new System.Drawing.Point(105, 12);
            this.jiraUrlText.Name = "jiraUrlText";
            this.jiraUrlText.Size = new System.Drawing.Size(279, 20);
            this.jiraUrlText.TabIndex = 15;
            // 
            // dashboardsListBox
            // 
            this.dashboardsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dashboardsListBox.FormattingEnabled = true;
            this.dashboardsListBox.Location = new System.Drawing.Point(12, 135);
            this.dashboardsListBox.Name = "dashboardsListBox";
            this.dashboardsListBox.Size = new System.Drawing.Size(372, 173);
            this.dashboardsListBox.TabIndex = 17;
            this.dashboardsListBox.SelectedIndexChanged += new System.EventHandler(this.OnDashboardsListBoxSelectedIndexChanged);
            // 
            // loadDashboardsButton
            // 
            this.loadDashboardsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.loadDashboardsButton.BackColor = System.Drawing.Color.Transparent;
            this.loadDashboardsButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.loadDashboardsButton.FlatAppearance.BorderSize = 0;
            this.loadDashboardsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loadDashboardsButton.Image = global::Jira.WallboardScreensaver.Properties.Resources.refresh_grey_18x18;
            this.loadDashboardsButton.Location = new System.Drawing.Point(365, 114);
            this.loadDashboardsButton.Name = "loadDashboardsButton";
            this.loadDashboardsButton.Size = new System.Drawing.Size(19, 21);
            this.loadDashboardsButton.TabIndex = 14;
            this.loadDashboardsButton.UseVisualStyleBackColor = false;
            this.loadDashboardsButton.Click += new System.EventHandler(this.OnLoadDashboardsButtonClicked);
            // 
            // EditPreferencesForm
            // 
            this.AcceptButton = saveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = cancelButton;
            this.ClientSize = new System.Drawing.Size(396, 394);
            this.Controls.Add(label6);
            this.Controls.Add(this.jiraUrlText);
            this.Controls.Add(this.loadDashboardsButton);
            this.Controls.Add(label5);
            this.Controls.Add(this.anonymousCheckbox);
            this.Controls.Add(label2);
            this.Controls.Add(this.loginPasswordText);
            this.Controls.Add(label4);
            this.Controls.Add(this.loginUsernameText);
            this.Controls.Add(label3);
            this.Controls.Add(saveButton);
            this.Controls.Add(cancelButton);
            this.Controls.Add(this.dashboardUrlText);
            this.Controls.Add(label1);
            this.Controls.Add(this.dashboardsListBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "EditPreferencesForm";
            this.Text = "JIRA Wallboard Screensaver";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox dashboardUrlText;
        private System.Windows.Forms.TextBox loginUsernameText;
        private System.Windows.Forms.TextBox loginPasswordText;
        private System.Windows.Forms.CheckBox anonymousCheckbox;
        private System.Windows.Forms.Button loadDashboardsButton;
        private System.Windows.Forms.TextBox jiraUrlText;
        private System.Windows.Forms.ListBox dashboardsListBox;
    }
}