using System;
using System.Windows.Forms;

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

            var filter = new UserActivityFilter();
            var form = new ScreensaverForm();
            new ScreensaverPresenter(filter, new TaskService()).Initialize(form);

            Application.AddMessageFilter(filter);
            Application.Run(form);
        }
    }
}
