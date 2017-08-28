using System;
using System.Threading.Tasks;

namespace Jira.WallboardScreensaver.Screensaver {
    public class ScreensaverPresenter {
        private IScreensaverView _view;
        private readonly UserActivityFilter _filter;
        private bool _ignoreUserActivity;

        public ScreensaverPresenter(UserActivityFilter filter)
        {
            _filter = filter;
        }

        public void Initialize(IScreensaverView view)
        {
            _view = view;
            _filter.UserActivity += OnUserActivity;
            _view.Closed += (obj, e) => _filter.UserActivity -= OnUserActivity;
            _view.Load += OnLoad;
        }

        public void Show()
        {
            _view.Show();
        }

        private void OnLoad(object sender, EventArgs e) {
            _ignoreUserActivity = true;
            Task.Delay(1000).ContinueWith(t => _ignoreUserActivity = false);
        }

        private void OnUserActivity()
        {
            if (_ignoreUserActivity) {
                return;
            }

            _view.Close();
        }
    }
}
