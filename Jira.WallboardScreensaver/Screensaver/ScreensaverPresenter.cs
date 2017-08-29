using System;
using System.Threading.Tasks;

namespace Jira.WallboardScreensaver.Screensaver {
    public class ScreensaverPresenter {
        private IScreensaverView _view;
        private readonly ConfigurationService _config;
        private readonly UserActivityFilter _filter;
        private readonly TaskService _task;
        private bool _startupDelayInProgress;

        public ScreensaverPresenter(ConfigurationService config, UserActivityFilter filter, TaskService task)
        {
            _config = config;
            _filter = filter;
            _task = task;
        }

        public void Initialize(IScreensaverView view)
        {
            _view = view;
            _filter.UserActivity += OnUserActivity;
            _view.Closed += (obj, e) => _filter.UserActivity -= OnUserActivity;
            _view.Load += OnLoad;
        }

        private void OnLoad(object sender, EventArgs e) {
            _startupDelayInProgress = true;
            _task.Delay(TimeSpan.FromSeconds(1)).ContinueWith(t => _startupDelayInProgress = false);

            if (_config.DashboardUri != null)
            {
                _view.Navigate(_config.DashboardUri.ToString());
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
