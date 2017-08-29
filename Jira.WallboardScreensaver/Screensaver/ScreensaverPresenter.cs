using System;
using System.Threading.Tasks;

namespace Jira.WallboardScreensaver.Screensaver {
    public class ScreensaverPresenter {
        private IScreensaverView _view;
        private readonly Preferences _preferences;
        private readonly BrowserService _browser;
        private readonly UserActivityFilter _filter;
        private readonly TaskService _task;
        private bool _startupDelayInProgress;

        public ScreensaverPresenter(
            Preferences preferences, 
            BrowserService browser, 
            UserActivityFilter filter,
            TaskService task)
        {
            _preferences = preferences;
            _browser = browser;
            _filter = filter;
            _task = task;
        }

        public void Initialize(IScreensaverView view)
        {
            _view = view;
            _filter.UserActivity += OnUserActivity;
            _view.Closed += (obj, e) => _filter.UserActivity -= OnUserActivity;
            _view.Load += OnLoad;

            _browser.ConfigureEmulation();

            if (_preferences.DashboardUri != null && _preferences.LoginCookies != null)
            {
                var baseUri = new Uri(_preferences.DashboardUri, "/");

                foreach (var cookie in _preferences.LoginCookies)
                {
                    _browser.SetCookie(baseUri, cookie.Key, cookie.Value);
                }
            }
        }

        private void OnLoad(object sender, EventArgs e) {
            _startupDelayInProgress = true;
            _task.Delay(TimeSpan.FromSeconds(1)).ContinueWith(t => _startupDelayInProgress = false);

            if (_preferences.DashboardUri != null)
            {
                _view.Navigate(_preferences.DashboardUri);
            }
        }

        private void OnUserActivity(object sender, EventArgs e)
        {
            if (ShouldIgnoreUserActivity()) {
                return;
            }

            _view.Close();
        }

        private bool ShouldIgnoreUserActivity()
        {
            return _startupDelayInProgress || _view.NavigationInProgress;
        }
    }
}
