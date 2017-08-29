using System;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Jira.WallboardScreensaver {
    using Screensaver;

    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args) {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form form;

            switch (args.FirstOrDefault())
            {
                case null: // Show preferences
                case "/c": // Show preferences
                    form = new Form();
                    break;
                case "/p": // Show preview (do nothing)
                    return;
                case "/s":
                    var key = Registry.CurrentUser.CreateSubKey(@"Software\Jira Wallboard Screensaver");
                    var filter = new UserActivityFilter { IdleTimeout = TimeSpan.FromSeconds(5) };
                    form = new ScreensaverForm();

                    new ScreensaverPresenter(
                        new PreferencesService(key).GetPreferences(),
                        new BrowserService(),
                        filter,
                        new TaskService()
                    ).Initialize((ScreensaverForm)form);

                    Application.AddMessageFilter(filter);

                    break;
                default:
                    throw new ArgumentException($"Unknown argument value: `${args[0]}`.");
            }

            Application.Run(form);
        }
    }
}
