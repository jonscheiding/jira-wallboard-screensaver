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
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Button cancelButton;
            System.Windows.Forms.Button saveButton;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label4;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditPreferencesForm));
            this.loginCookiesText = new System.Windows.Forms.TextBox();
            this.dashboardUrlText = new System.Windows.Forms.TextBox();
            this.loginUsernameText = new System.Windows.Forms.TextBox();
            this.loginPasswordText = new System.Windows.Forms.TextBox();
            this.loginButton = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            cancelButton = new System.Windows.Forms.Button();
            saveButton = new System.Windows.Forms.Button();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 15);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(87, 13);
            label1.TabIndex = 0;
            label1.Text = "Dashboard URL:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(12, 124);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(77, 13);
            label2.TabIndex = 1;
            label2.Text = "Login Cookies:";
            // 
            // cancelButton
            // 
            cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Location = new System.Drawing.Point(326, 322);
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
            saveButton.Location = new System.Drawing.Point(245, 322);
            saveButton.Name = "saveButton";
            saveButton.Size = new System.Drawing.Size(75, 23);
            saveButton.TabIndex = 5;
            saveButton.Text = "OK";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += new System.EventHandler(this.OnSaveButtonClicked);
            // 
            // loginCookiesText
            // 
            this.loginCookiesText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loginCookiesText.Location = new System.Drawing.Point(118, 121);
            this.loginCookiesText.Multiline = true;
            this.loginCookiesText.Name = "loginCookiesText";
            this.loginCookiesText.ReadOnly = true;
            this.loginCookiesText.Size = new System.Drawing.Size(283, 195);
            this.loginCookiesText.TabIndex = 2;
            // 
            // dashboardUrlText
            // 
            this.dashboardUrlText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dashboardUrlText.Location = new System.Drawing.Point(118, 12);
            this.dashboardUrlText.Name = "dashboardUrlText";
            this.dashboardUrlText.Size = new System.Drawing.Size(283, 20);
            this.dashboardUrlText.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(15, 42);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(58, 13);
            label3.TabIndex = 6;
            label3.Text = "Username:";
            // 
            // loginUsernameText
            // 
            this.loginUsernameText.Location = new System.Drawing.Point(118, 39);
            this.loginUsernameText.Name = "loginUsernameText";
            this.loginUsernameText.Size = new System.Drawing.Size(283, 20);
            this.loginUsernameText.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(15, 69);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(56, 13);
            label4.TabIndex = 8;
            label4.Text = "Password:";
            // 
            // loginPasswordText
            // 
            this.loginPasswordText.Location = new System.Drawing.Point(118, 66);
            this.loginPasswordText.Name = "loginPasswordText";
            this.loginPasswordText.PasswordChar = '●';
            this.loginPasswordText.Size = new System.Drawing.Size(283, 20);
            this.loginPasswordText.TabIndex = 9;
            // 
            // loginButton
            // 
            this.loginButton.Location = new System.Drawing.Point(326, 92);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(75, 23);
            this.loginButton.TabIndex = 10;
            this.loginButton.Text = "Log In";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(this.OnLoginButtonClick);
            // 
            // EditPreferencesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 357);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.loginPasswordText);
            this.Controls.Add(label4);
            this.Controls.Add(this.loginUsernameText);
            this.Controls.Add(label3);
            this.Controls.Add(saveButton);
            this.Controls.Add(cancelButton);
            this.Controls.Add(this.dashboardUrlText);
            this.Controls.Add(this.loginCookiesText);
            this.Controls.Add(label2);
            this.Controls.Add(label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditPreferencesForm";
            this.Text = "EditPreferencesForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox loginCookiesText;
        private System.Windows.Forms.TextBox dashboardUrlText;
        private System.Windows.Forms.TextBox loginUsernameText;
        private System.Windows.Forms.TextBox loginPasswordText;
        private System.Windows.Forms.Button loginButton;
    }
}