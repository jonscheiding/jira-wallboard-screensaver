using System;

namespace Jira.WallboardScreensaver.Screensaver {
    public class ScreensaverPresenter : IPresenter<IScreensaverView> {
        private readonly IBrowserService _browser;
        private readonly Preferences _preferences;
        private readonly ITaskService _task;
        private readonly IUserActivityService _userActivity;
        private bool _ignoringUserActivity;
        private IScreensaverView _view;

        public ScreensaverPresenter(
            Preferences preferences,
            IBrowserService browser,
            IUserActivityService userActivity,
            ITaskService task) {
            _preferences = preferences;
            _browser = browser;
            _userActivity = userActivity;
            _task = task;
        }

        public void Initialize(IScreensaverView view) {
            _view = view;
            _view.Load += OnLoad;
            _view.ExitButtonClicked += (obj, e) => _view.Close();

            _userActivity.UserActive += OnUserActive;
            _userActivity.UserIdle += OnUserIdle;
            _view.Closed += (obj, e) => {
                _userActivity.UserActive -= OnUserActive;
                _userActivity.UserIdle -= OnUserIdle;
            };

            _browser.ConfigureEmulation();

            if (_preferences.DashboardUri != null && _preferences.LoginCookies != null) {
                var baseUri = new Uri(_preferences.DashboardUri, "/");

                foreach (var cookie in _preferences.LoginCookies)
                    _browser.SetCookie(baseUri, cookie.Key, cookie.Value);
            }
        }

        private void OnLoad(object sender, EventArgs e) {
            BrieflyIgnoreUserActivity();

            if (_preferences.DashboardUri != null)
                _view.Navigate(_preferences.DashboardUri);
        }

        private void OnUserActive(object sender, EventArgs e) {
            if (ShouldIgnoreUserActivity())
                return;

            _view.ControlsVisible = true;
        }

        private void OnUserIdle(object sender, EventArgs e) {
            _view.ControlsVisible = false;
            BrieflyIgnoreUserActivity();
        }

        private void BrieflyIgnoreUserActivity() {
            _ignoringUserActivity = true;
            _task.Delay(TimeSpan.FromSeconds(1))
                .ContinueWith(t => _ignoringUserActivity = false);
        }

        private bool ShouldIgnoreUserActivity() {
            return _ignoringUserActivity || _view.NavigationInProgress;
        }
    }
}