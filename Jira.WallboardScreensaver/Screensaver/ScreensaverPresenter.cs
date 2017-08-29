using System;

namespace Jira.WallboardScreensaver.Screensaver {
    public class ScreensaverPresenter {
        private IScreensaverView _view;
        private readonly Preferences _preferences;
        private readonly BrowserService _browser;
        private readonly UserActivityFilter _filter;
        private readonly TaskService _task;
        private bool _ignoringUserActivity;

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
            _view.Load += OnLoad;
            _view.ExitButtonClicked += (obj, e) => _view.Close();

            _filter.UserActive += OnUserActive;
            _filter.UserIdle += OnUserIdle;
            _view.Closed += (obj, e) =>
            {
                _filter.UserActive -= OnUserActive;
                _filter.UserIdle -= OnUserIdle;
            };

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

        private void OnLoad(object sender, EventArgs e)
        {
            BrieflyIgnoreUserActivity();

            if (_preferences.DashboardUri != null)
            {
                _view.Navigate(_preferences.DashboardUri);
            }
        }

        private void OnUserActive(object sender, EventArgs e)
        {
            if (ShouldIgnoreUserActivity())
            {
                return;
            }

            _view.ControlsVisible = true;
        }

        private void OnUserIdle(object sender, EventArgs e) {
            _view.ControlsVisible = false;
            BrieflyIgnoreUserActivity();
        }

        private void BrieflyIgnoreUserActivity()
        {
            _ignoringUserActivity = true;
            _task.Delay(TimeSpan.FromSeconds(1))
                .ContinueWith(t => _ignoringUserActivity = false);
        }

        private bool ShouldIgnoreUserActivity()
        {
            return _ignoringUserActivity || _view.NavigationInProgress;
        }

    }
}
