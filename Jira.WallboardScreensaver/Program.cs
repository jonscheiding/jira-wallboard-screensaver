using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Autofac;
using Microsoft.Win32;

namespace Jira.WallboardScreensaver {
    using Screensaver;

    internal static class Program {
        private static readonly IContainer Container = BuildContainer();

        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            var assembly = Assembly.GetExecutingAssembly();

            builder.RegisterInstance(Registry.CurrentUser.CreateSubKey(@"Software\Jira Wallboard Screensaver"));

            builder.RegisterType<UserActivityFilter>()
                .WithProperty(nameof(UserActivityFilter.IdleTimeout), TimeSpan.FromSeconds(3))
                .OnActivated(e => Application.AddMessageFilter(e.Instance));

            builder.RegisterAdapter<PreferencesService, Preferences>(svc => svc.GetPreferences());

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Presenter"))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsSelf();

            return builder.Build();
        }

        private static TForm Present<TForm, TView>() where TForm : Form, TView, new()
        {
            var form = new TForm();
            var presenter = Container.Resolve<IPresenter<TView>>();
            presenter.Initialize(form);
            return form;
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args) {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form form;

            switch (args.Select(a => a.ToLowerInvariant()).FirstOrDefault())
            {
                case null: // Show preferences
                case "/c": // Show preferences
                    form = new Form();
                    break;
                case "/p": // Show preview (do nothing)
                    return;
                case "/s":
                    form = Present<ScreensaverForm, IScreensaverView>();

                    break;
                default:
                    throw new ArgumentException($"Unknown argument value: `{args[0]}`.");
            }

            Application.Run(form);
        }
    }
}
