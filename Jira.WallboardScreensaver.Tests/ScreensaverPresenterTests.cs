﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Jira.WallboardScreensaver.Screensaver;
using NSubstitute;
using NUnit.Framework;

namespace Jira.WallboardScreensaver.Tests
{
    [TestFixture]
    public class ScreensaverPresenterTests
    {
        private IScreensaverView _view;
        private UserActivityFilter _filter;
        private ScreensaverPresenter _presenter;
        private TaskCompletionSource<object> _task;
        private TaskService _taskService;
        private ConfigurationService _configService;

        [SetUp]
        public void SetUp()
        {
            _task = new TaskCompletionSource<object>();
            _taskService = Substitute.For<TaskService>();
            _configService = Substitute.For<ConfigurationService>();
            _view = Substitute.For<IScreensaverView>();
            _filter = Substitute.For<UserActivityFilter>();

            _taskService.Delay(Arg.Any<TimeSpan>()).Returns(_task.Task);

            _presenter = new ScreensaverPresenter(_configService, _filter, _taskService);
            _presenter.Initialize(_view);
        }

        [Test]
        public void DoesNotCloseOnUserActivityIfOpenTimeoutHasNotElapsed()
        {
            //

            _view.Load += Raise.Event();
            _filter.UserActivity += Raise.Event();
            
            //

            _view.DidNotReceive().Close();
        }

        [Test]
        public void DoesNotCloseOnUserActivityIfNavigationIsInProgress()
        {
            _view.NavigationInProgress.Returns(true);

            //

            _view.Load += Raise.Event();
            _filter.UserActivity += Raise.Event();

            //

            _view.DidNotReceive().Close();
        }

        [Test]
        public void ClosesOnUserActivityAfterOpenTimeoutOfOneSecondHasElapsed()
        {
            //

            _view.Load += Raise.Event();
            _task.SetResult(null);
            Thread.Sleep(100);
            _filter.UserActivity += Raise.Event();

            //

            _view.Received(1).Close();
            _taskService.Received().Delay(TimeSpan.FromSeconds(1));
        }

        [Test]
        public void DisplaysConfiguredUrlOnLoad()
        {
            var url = "http://google.com/";
            _configService.DashboardUri.Returns(new Uri(url));

            //

            _view.Load += Raise.Event();

            //

            _view.Received(1).Navigate(url);
        }
    }
}