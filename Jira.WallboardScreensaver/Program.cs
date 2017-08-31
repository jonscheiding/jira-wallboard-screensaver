using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Autofac;
using Jira.WallboardScreensaver.EditPreferences;
using Jira.WallboardScreensaver.Screensaver;
using Jira.WallboardScreensaver.Services;
using Microsoft.Win32;

namespace Jira.WallboardScreensaver {
    internal static class Program {
        private static readonly IContainer Container = BuildContainer();

        private static IContainer BuildContainer() {
            var builder = new ContainerBuilder();
            var assembly = Assembly.GetExecutingAssembly();

            builder.RegisterInstance(Registry.CurrentUser.CreateSubKey(@"Software\Jira Wallboard Screensaver"));

            builder.RegisterType<UserActivityService>()
                .WithProperty(nameof(UserActivityService.IdleTimeout), TimeSpan.FromSeconds(3))
                .OnActivated(e => Application.AddMessageFilter(e.Instance))
                .AsImplementedInterfaces();

            builder.RegisterAdapter<PreferencesService, Preferences>(svc => svc.GetPreferences());

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Presenter"))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Service") && t != typeof(UserActivityService))
                .AsImplementedInterfaces();

            return builder.Build();
        }

        private static TForm Present<TForm, TView>() where TForm : Form, TView, new() {
            var form = new TForm();
            var presenter = Container.Resolve<IPresenter<TView>>();
            presenter.Initialize(form);
            return form;
        }

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args) {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            switch (args.Select(a => a.ToLowerInvariant()).FirstOrDefault()) {
                case null: // Show preferences
                case "/c": // Show preferences
                    Application.Run(Present<EditPreferencesForm, IEditPreferencesView>());
                    break;
                case "/p": // Show preview (do nothing)
                    return;
                case "/s":
                    Application.Run(Present<ScreensaverForm, IScreensaverView>());
                    break;
                default:
                    throw new ArgumentException($"Unknown argument value: `{args[0]}`.");
            }
        }
    }
}