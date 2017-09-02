using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Security.Policy;
using System.Threading.Tasks;
using Jira.WallboardScreensaver.EditPreferences;
using Jira.WallboardScreensaver.Services;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;

namespace Jira.WallboardScreensaver.Tests {
    [TestFixture]
    public class EditPreferencesPresenterTests {
        private EditPreferencesPresenter _presenter;
        private IEditPreferencesView _view;
        private IPreferencesService _preferences;
        private IJiraService _jira;
        
        private static Task TaskThatNeverCompletes<T>() {
            return new TaskCompletionSource<T>().Task;
        }

        [SetUp]
        public void SetUp() {
            _preferences = Substitute.For<IPreferencesService>();
            _view = Substitute.For<IEditPreferencesView>();
            _jira = Substitute.For<IJiraService>();

            _presenter = new EditPreferencesPresenter(_preferences, _jira);
        }

        [Test]
        public void CancelClosesView() {
            _preferences.GetPreferences().Returns(new Preferences());
            _presenter.Initialize(_view);

            //

            _view.CancelButtonClicked += Raise.Event();

            //

            _view.Received().Close();
        }

        [Test]
        public void CancelClosesViewIfPreferencesAreNotValid() {
            _preferences.GetPreferences().Returns(new Preferences());
            _presenter.Initialize(_view);
            _view.DashboardUrl.Returns("bad");

            //

            _view.CancelButtonClicked += Raise.Event();

            //

            _view.Received().Close();
        }

        [Test]
        public void DoesNotSavePreferencesWhenCancelButtonClicked() {
            _preferences.GetPreferences().Returns(new Preferences());

            //

            _presenter.Initialize(_view);
            _view.CancelButtonClicked += Raise.Event();

            //

            _preferences.DidNotReceive().SetPreferences(Arg.Any<Preferences>());
        }

        [Test]
        public void LoadsPreferencesIntoView() {
            var p = new Preferences {
                DashboardUri = new Uri("http://www.google.com/")
            };

            _preferences.GetPreferences().Returns(p);

            //

            _presenter.Initialize(_view);

            //

            _preferences.Received().GetPreferences();
            _view.Received().DashboardUrl = "http://www.google.com/";
        }

        [Test]
        public void SaveDoesNotCloseViewIfUrlIsNotValid() {
            _preferences.GetPreferences().Returns(new Preferences());
            _presenter.Initialize(_view);
            _view.DashboardUrl.Returns("bad");

            //

            _view.SaveButtonClicked += Raise.Event();

            //

            _view.DidNotReceive().Close();
        }

        [Test]
        public void SavesUrlFromViewWhenSaveButtonClicked() {
            _preferences.GetPreferences().Returns(new Preferences());
            _presenter.Initialize(_view);

            _view.DashboardUrl.Returns("http://www.google.com/");
            _view.LoginUsername.Returns("user");
            _view.LoginPassword.Returns("pass");

            //

            _view.SaveButtonClicked += Raise.Event();

            //

            _preferences.Received()
                .SetPreferences(Arg.Is<Preferences>(p => 
                    p.DashboardUri.Equals(new Uri("http://www.google.com/"))
                ));
        }

        [Test]
        public void ShowsErrorIfDashboardUriIsNotSet() {
            _preferences.GetPreferences().Returns(new Preferences());
            _presenter.Initialize(_view);
            _view.DashboardUrl.Returns("");

            //

            _view.SaveButtonClicked += Raise.Event();

            //

            _view.Received().ShowError(Arg.Any<string>());
        }

        [Test]
        public void ShowsErrorIfDashboardUriIsNotValid() {
            _preferences.GetPreferences().Returns(new Preferences());
            _presenter.Initialize(_view);
            _view.DashboardUrl.Returns("bad");

            //

            _view.SaveButtonClicked += Raise.Event();

            //

            _view.Received().ShowError(Arg.Any<string>());
        }

        [Test]
        public void LogsInToJiraWhenSaveButtonIsClicked() {
            _preferences.GetPreferences().Returns(new Preferences());
            _presenter.Initialize(_view);

            _view.DashboardUrl.Returns("http://www.google.com/somewhere");
            _view.LoginUsername.Returns("user");
            _view.LoginPassword.Returns("pass");

            //

            _view.SaveButtonClicked += Raise.Event();

            //

            _jira.Received().Login(new Uri("http://www.google.com/"), "user", "pass");
        }

        [Test]
        public void DisablesViewWhileLoggingInToJira() {
            _preferences.GetPreferences().Returns(new Preferences());
            _presenter.Initialize(_view);
            _jira.Login(Arg.Any<Uri>(), Arg.Any<string>(), Arg.Any<string>())
                .Returns(TaskThatNeverCompletes<IReadOnlyDictionary<string, string>>());

            _view.DashboardUrl.Returns("http://www.google.com/somewhere");
            _view.LoginUsername.Returns("user");
            _view.LoginPassword.Returns("pass");

            //

            _view.SaveButtonClicked += Raise.Event();

            //

            _view.Received().Disabled = true;
        }

        [Test]
        public void SavesCookiesFromJiraLoginWhenSaveButtonClicked() {
            _preferences.GetPreferences().Returns(new Preferences());
            _presenter.Initialize(_view);

            _view.DashboardUrl.Returns("http://www.google.com/somewhere");
            _view.LoginUsername.Returns("user");
            _view.LoginPassword.Returns("pass");

            var cookies = Substitute.For<IReadOnlyDictionary<string, string>>();
            _jira.Login(Arg.Any<Uri>(), Arg.Any<string>(), Arg.Any<string>())
                .Returns(Task.FromResult(cookies));

            //

            _view.SaveButtonClicked += Raise.Event();

            //

            _preferences.Received().SetPreferences(Arg.Is<Preferences>(p => p.LoginCookies == cookies));
        }

        [Test]
        public void ClosesViewAfterSavingPreferences() {
            _preferences.GetPreferences().Returns(new Preferences());
            _presenter.Initialize(_view);

            _view.DashboardUrl.Returns("http://www.google.com/somewhere");
            _view.LoginUsername.Returns("user");
            _view.LoginPassword.Returns("pass");

            var cookies = Substitute.For<IReadOnlyDictionary<string, string>>();
            _jira.Login(Arg.Any<Uri>(), Arg.Any<string>(), Arg.Any<string>())
                .Returns(Task.FromResult(cookies));

            //

            _view.SaveButtonClicked += Raise.Event();

            //

            _view.Received().Close();
        }

        [Test]
        public void ShowsErrorMessageIfUsernameIsNotSet() {
            _preferences.GetPreferences().Returns(new Preferences());
            _presenter.Initialize(_view);

            _view.DashboardUrl.Returns("http://www.google.com/somewhere");
            _view.LoginUsername.Returns(string.Empty);
            _view.LoginPassword.Returns("pass");

            //

            _view.SaveButtonClicked += Raise.Event();

            //

            _view.Received().ShowError(Arg.Any<string>());
            _view.DidNotReceive().Close();
            _preferences.DidNotReceive().SetPreferences(Arg.Any<Preferences>());
        }

        [Test]
        public void ShowsErrorMessageIfPasswordIsNotSet() {
            _preferences.GetPreferences().Returns(new Preferences());
            _presenter.Initialize(_view);

            _view.DashboardUrl.Returns("http://www.google.com/somewhere");
            _view.LoginUsername.Returns("user");
            _view.LoginPassword.Returns(string.Empty);

            //

            _view.SaveButtonClicked += Raise.Event();
            
            //

            _view.Received().ShowError(Arg.Any<string>());
            _view.DidNotReceive().Close();
            _preferences.DidNotReceive().SetPreferences(Arg.Any<Preferences>());
        }

        [Test]
        public void ShowsErrorMessageIfLoginFails() {
            _preferences.GetPreferences().Returns(new Preferences());
            _presenter.Initialize(_view);

            _view.DashboardUrl.Returns("http://www.google.com/somewhere");
            _view.LoginUsername.Returns("user");
            _view.LoginPassword.Returns("pass");

            _jira.Login(Arg.Any<Uri>(), Arg.Any<string>(), Arg.Any<string>())
                .Throws(new HttpRequestException());

            //

            _view.SaveButtonClicked += Raise.Event();

            //

            _view.Received().ShowError(Arg.Any<string>());
            _view.DidNotReceive().Close();
            _preferences.DidNotReceive().SetPreferences(Arg.Any<Preferences>());
        }
    }
}