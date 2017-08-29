using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Jira.WallboardScreensaver {
    using Screensaver;

    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var key = Registry.CurrentUser.CreateSubKey(@"Software\Jira Wallboard Screensaver");

            var filter = new UserActivityFilter();
            var form = new ScreensaverForm();
            new ScreensaverPresenter(
                new PreferencesService(key).GetPreferences(), 
                new BrowserService(), 
                filter, 
                new TaskService()
            ).Initialize(form);

            Application.AddMessageFilter(filter);
            Application.Run(form);
        }
    }
}
