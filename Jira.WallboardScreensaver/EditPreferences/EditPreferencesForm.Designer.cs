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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditPreferencesForm));
            this.dashboardUrlText = new System.Windows.Forms.TextBox();
            this.loginUsernameText = new System.Windows.Forms.TextBox();
            this.loginPasswordText = new System.Windows.Forms.TextBox();
            this.anonymousCheckbox = new System.Windows.Forms.CheckBox();
            label1 = new System.Windows.Forms.Label();
            cancelButton = new System.Windows.Forms.Button();
            saveButton = new System.Windows.Forms.Button();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(16, 18);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(114, 17);
            label1.TabIndex = 0;
            label1.Text = "Dashboard URL:";
            // 
            // cancelButton
            // 
            cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Location = new System.Drawing.Point(433, 239);
            cancelButton.Margin = new System.Windows.Forms.Padding(4);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(100, 28);
            cancelButton.TabIndex = 4;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += new System.EventHandler(this.OnCancelButtonClicked);
            // 
            // saveButton
            // 
            saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            saveButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            saveButton.Location = new System.Drawing.Point(325, 239);
            saveButton.Margin = new System.Windows.Forms.Padding(4);
            saveButton.Name = "saveButton";
            saveButton.Size = new System.Drawing.Size(100, 28);
            saveButton.TabIndex = 5;
            saveButton.Text = "OK";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += new System.EventHandler(this.OnSaveButtonClicked);
            // 
            // label3
            // 
            label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(20, 182);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(77, 17);
            label3.TabIndex = 6;
            label3.Text = "Username:";
            // 
            // label4
            // 
            label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(20, 212);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(73, 17);
            label4.TabIndex = 8;
            label4.Text = "Password:";
            // 
            // label2
            // 
            label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(20, 154);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(86, 17);
            label2.TabIndex = 10;
            label2.Text = "Anonymous:";
            // 
            // dashboardUrlText
            // 
            this.dashboardUrlText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dashboardUrlText.Location = new System.Drawing.Point(157, 15);
            this.dashboardUrlText.Margin = new System.Windows.Forms.Padding(4);
            this.dashboardUrlText.Multiline = true;
            this.dashboardUrlText.Name = "dashboardUrlText";
            this.dashboardUrlText.Size = new System.Drawing.Size(376, 133);
            this.dashboardUrlText.TabIndex = 3;
            // 
            // loginUsernameText
            // 
            this.loginUsernameText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loginUsernameText.Location = new System.Drawing.Point(157, 179);
            this.loginUsernameText.Margin = new System.Windows.Forms.Padding(4);
            this.loginUsernameText.Name = "loginUsernameText";
            this.loginUsernameText.Size = new System.Drawing.Size(376, 22);
            this.loginUsernameText.TabIndex = 7;
            // 
            // loginPasswordText
            // 
            this.loginPasswordText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loginPasswordText.Location = new System.Drawing.Point(157, 209);
            this.loginPasswordText.Margin = new System.Windows.Forms.Padding(4);
            this.loginPasswordText.Name = "loginPasswordText";
            this.loginPasswordText.PasswordChar = '●';
            this.loginPasswordText.Size = new System.Drawing.Size(376, 22);
            this.loginPasswordText.TabIndex = 9;
            // 
            // anonymousCheckbox
            // 
            this.anonymousCheckbox.AutoSize = true;
            this.anonymousCheckbox.Location = new System.Drawing.Point(157, 155);
            this.anonymousCheckbox.Name = "anonymousCheckbox";
            this.anonymousCheckbox.Size = new System.Drawing.Size(18, 17);
            this.anonymousCheckbox.TabIndex = 11;
            this.anonymousCheckbox.UseVisualStyleBackColor = true;
            this.anonymousCheckbox.CheckedChanged += new System.EventHandler(this.OnAnonymousCheckboxChanged);
            // 
            // EditPreferencesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 280);
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
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
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
    }
}