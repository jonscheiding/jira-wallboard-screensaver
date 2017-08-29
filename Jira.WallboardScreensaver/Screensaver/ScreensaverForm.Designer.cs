using System.Windows.Forms;

namespace Jira.WallboardScreensaver.Screensaver {
    partial class ScreensaverForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
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
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.exitButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(0, 0);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.ScriptErrorsSuppressed = true;
            this.webBrowser.Size = new System.Drawing.Size(284, 261);
            this.webBrowser.TabIndex = 0;
            this.webBrowser.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.OnWebBrowserNavigated);
            this.webBrowser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.OnWebBrowserNavigating);
            // 
            // exitButton
            // 
            this.exitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.exitButton.Location = new System.Drawing.Point(245, 13);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(27, 23);
            this.exitButton.TabIndex = 1;
            this.exitButton.Text = "X";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Visible = false;
            this.exitButton.Click += new System.EventHandler(this.OnExitButtonClick);
            // 
            // ScreensaverForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.webBrowser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ScreensaverForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private WebBrowser webBrowser;
        private Button exitButton;
    }
}

