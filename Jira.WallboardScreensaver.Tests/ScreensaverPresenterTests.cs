using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Jira.WallboardScreensaver.Screensaver;
using NSubstitute;
using NUnit.Framework;

namespace Jira.WallboardScreensaver.Tests {
    [TestFixture]
    public class ScreensaverPresenterTests {
        [SetUp]
        public void SetUp() {
            _taskService = Substitute.For<ITaskService>();
            _browserService = Substitute.For<IBrowserService>();
            _view = Substitute.For<IScreensaverView>();
            _filter = Substitute.For<IUserActivityService>();
            _preferences = Substitute.For<Preferences>();

            _taskService.Delay(Arg.Any<TimeSpan>()).Returns(Task.CompletedTask);

            _presenter = new ScreensaverPresenter(_preferences, _browserService, _filter, _taskService);
        }

        private IScreensaverView _view;
        private IUserActivityService _filter;
        private ITaskService _taskService;
        private IBrowserService _browserService;
        private Preferences _preferences;
        private ScreensaverPresenter _presenter;

        private Task TaskThatNeverCompletes() {
            return new TaskCompletionSource<object>().Task;
        }

        [Test]
        public void ClosesWhenExitButtonClicked() {
            _presenter.Initialize(_view);
            _view.Load += Raise.Event();

            //

            _view.ExitButtonClicked += Raise.Event();

            //

            _view.Received(1).Close();
        }

        [Test]
        public void DisplaysConfiguredUrlOnLoad() {
            var uri = new Uri("http://google.com/");
            _preferences.DashboardUri.Returns(uri);

            //

            _presenter.Initialize(_view);
            _view.Load += Raise.Event();

            //

            _view.Received(1).Navigate(uri);
        }

        [Test]
        public void HidesControlsWhenUserIdleDetected() {
            _presenter.Initialize(_view);
            _view.Load += Raise.Event();

            //

            _filter.UserActive += Raise.Event();
            _filter.UserIdle += Raise.Event();

            //

            _view.Received(1).ControlsVisible = false;
        }

        [Test]
        public void IgnoresUserActivityForOneSecondAfterInitialization() {
            _taskService.Delay(Arg.Any<TimeSpan>()).Returns(TaskThatNeverCompletes());
            _presenter.Initialize(_view);
            _view.Load += Raise.Event();

            //

            _filter.UserActive += Raise.Event();

            //

            _view.Received(0).ControlsVisible = true;
            _taskService.Received(1).Delay(TimeSpan.FromSeconds(1));
        }

        [Test]
        public void IgnoresUserActivityForOneSecondAfterUserIdle() {
            _presenter.Initialize(_view);
            _view.Load += Raise.Event();

            _taskService.Delay(Arg.Any<TimeSpan>()).Returns(TaskThatNeverCompletes());

            //

            _filter.UserIdle += Raise.Event();
            Thread.Sleep(500);
            _filter.UserActive += Raise.Event();

            //

            _view.Received(0).ControlsVisible = true;
            _taskService.Received(2).Delay(TimeSpan.FromSeconds(1));
        }

        [Test]
        public void SetsCookiesAndConfiguresEmulationOnInitialize() {
            var uri = new Uri("http://www.google.com/some_uri");
            var baseUri = new Uri("http://www.google.com/");
            var cookies = new Dictionary<string, string> {
                {"cookie1", "value1"},
                {"cookie2", "value2"}
            };

            _preferences.DashboardUri.Returns(uri);
            _preferences.LoginCookies.Returns(cookies);

            //

            _presenter.Initialize(_view);

            //

            _browserService.Received(1).SetCookie(baseUri, "cookie1", "value1");
            _browserService.Received(1).SetCookie(baseUri, "cookie2", "value2");
            _browserService.Received(1).ConfigureEmulation();
        }

        [Test]
        public void ShowsControlsWhenUserActivityDetected() {
            _presenter.Initialize(_view);
            _view.Load += Raise.Event();

            //

            _filter.UserActive += Raise.Event();

            //

            _view.Received(1).ControlsVisible = true;
        }
    }
}