using System;
using System.Threading.Tasks;

namespace Jira.WallboardScreensaver.Screensaver {
    public class ScreensaverPresenter {
        private IScreensaverView _view;
        private readonly UserActivityFilter _filter;
        private readonly TaskService _task;
        private bool _startupDelayInProgress;

        public ScreensaverPresenter(UserActivityFilter filter, TaskService task)
        {
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

            _view.Navigate("http://www.google.com");
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
