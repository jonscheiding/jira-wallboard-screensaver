using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Jira.WallboardScreensaver.EditPreferences;
using Jira.WallboardScreensaver.Services;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;

using JiraCredentials = System.Collections.Generic.IReadOnlyDictionary<string, string>;

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
            _view.Anonymous.Returns(false);

            //

            _view.SaveButtonClicked += Raise.Event();

            //

            _jira.Received().LoginAsync(new Uri("http://www.google.com/"), "user", "pass");
        }

        [Test]
        public void LogsInToJiraWhenLoadDashboardsButtonIsClicked() {
            _preferences.GetPreferences().Returns(new Preferences());
            _presenter.Initialize(_view);

            _view.JiraUrl.Returns("http://some-jira.atlassian.net");
            _view.LoginUsername.Returns("user");
            _view.LoginPassword.Returns("pass");
            _view.Anonymous.Returns(false);

            //

            _view.LoadDashboardsButtonClicked += Raise.Event();

            //

            _jira.Received().LoginAsync(new Uri("http://some-jira.atlassian.net/"), "user", "pass");
        }

        [Test]
        public void LoadsDashboardsFromJiraAfterLoggingIn() {
            _preferences.GetPreferences().Returns(new Preferences());
            _presenter.Initialize(_view);

            var cookies = Substitute.For<JiraCredentials>();

            _view.JiraUrl.Returns("http://some-jira.atlassian.net");
            _view.LoginUsername.Returns("user");
            _view.LoginPassword.Returns("pass");
            _view.Anonymous.Returns(false);
            _jira.LoginAsync(Arg.Any<Uri>(), Arg.Any<string>(), Arg.Any<string>())
                .Returns(Task.FromResult(cookies));

            //

            _view.LoadDashboardsButtonClicked += Raise.Event();

            //

            _jira.Received().GetDashboardsAsync(new Uri("http://some-jira.atlassian.net/"), cookies);
        }

        [Test]
        public void DoesNotLoginBeforeLoadingDashboardsIfAnonymousIsChecked() {
            _preferences.GetPreferences().Returns(new Preferences());
            _presenter.Initialize(_view);

            _view.JiraUrl.Returns("http://some-jira.atlassian.net");
            _view.Anonymous.Returns(true);

            //

            _view.LoadDashboardsButtonClicked += Raise.Event();

            //

            _jira.Received()
                .GetDashboardsAsync(
                    new Uri("http://some-jira.atlassian.net/"),
                    Arg.Is<JiraCredentials>(cookies => cookies.Count == 0));
        }

        [Test]
        public void PutsDashboardsIntoViewWhenTheyAreLoaded() {
            _preferences.GetPreferences().Returns(new Preferences());
            _presenter.Initialize(_view);

            var dashboards = new JiraDashboard[0];

            _view.JiraUrl.Returns("http://some-jira.atlassian.net");
            _view.Anonymous.Returns(true);
            _jira.GetDashboardsAsync(Arg.Any<Uri>(), Arg.Any<JiraCredentials>())
                .Returns(Task.FromResult(dashboards.AsEnumerable()));

            //

            _view.LoadDashboardsButtonClicked += Raise.Event();

            //

            // ReSharper disable once CoVariantArrayConversion
            _view.Received().SetDashboardItems(Arg.Do<IDashboardDisplayItem[]>(arg => 
                Assert.That(arg, Is.EquivalentTo(dashboards))));
        }

        [Test]
        public void DisablesViewWhileLoggingInToJira() {
            _preferences.GetPreferences().Returns(new Preferences());
            _presenter.Initialize(_view);
            _jira.LoginAsync(Arg.Any<Uri>(), Arg.Any<string>(), Arg.Any<string>())
                .Returns(TaskThatNeverCompletes<JiraCredentials>());

            _view.DashboardUrl.Returns("http://www.google.com/somewhere");
            _view.LoginUsername.Returns("user");
            _view.LoginPassword.Returns("pass");
            _view.Anonymous.Returns(false);

            //

            _view.SaveButtonClicked += Raise.Event();

            //

            _view.Received().Disabled = true;
        }

        [Test]
        public void ReEnablesViewAfterLoadingDashboards() {
            _preferences.GetPreferences().Returns(new Preferences());
            _presenter.Initialize(_view);

            _view.JiraUrl.Returns("http://some-jira.atlassian.net");
            _view.LoginUsername.Returns("user");
            _view.LoginPassword.Returns("pass");
            _view.Anonymous.Returns(false);

            //

            _view.LoadDashboardsButtonClicked += Raise.Event();

            //

            _view.Received().Disabled = false;
        }

        [Test]
        public void SetsDashboardUrlWhenADashboardIsSelected() {
            _preferences.GetPreferences().Returns(new Preferences());
            _presenter.Initialize(_view);

            _view.SelectedDashboardItem.Returns(new JiraDashboard("Test", 1));
            _view.JiraUrl.Returns("http://test.atlassian.net");

            //

            _view.SelectedDashboardItemChanged += Raise.Event();

            //

            _view.Received().DashboardUrl = "http://test.atlassian.net/plugins/servlet/Wallboard/?dashboardId=1";
        }

        [Test]
        public void SavesCookiesAndUsernameFromJiraLoginWhenSaveButtonClicked() {
            _preferences.GetPreferences().Returns(new Preferences());
            _presenter.Initialize(_view);

            _view.DashboardUrl.Returns("http://www.google.com/somewhere");
            _view.LoginUsername.Returns("user");
            _view.LoginPassword.Returns("pass");
            _view.Anonymous.Returns(false);

            var cookies = Substitute.For<JiraCredentials>();
            _jira.LoginAsync(Arg.Any<Uri>(), Arg.Any<string>(), Arg.Any<string>())
                .Returns(Task.FromResult(cookies));

            //

            _view.SaveButtonClicked += Raise.Event();

            //

            _preferences.Received()
                .SetPreferences(Arg.Is<Preferences>(p =>
                    p.LoginCookies == cookies &&
                    p.LoginUsername == "user"));
        }

        [Test]
        public void ClosesViewAfterSavingPreferences() {
            _preferences.GetPreferences().Returns(new Preferences());
            _presenter.Initialize(_view);

            _view.DashboardUrl.Returns("http://www.google.com/somewhere");
            _view.LoginUsername.Returns("user");
            _view.LoginPassword.Returns("pass");

            var cookies = Substitute.For<JiraCredentials>();
            _jira.LoginAsync(Arg.Any<Uri>(), Arg.Any<string>(), Arg.Any<string>())
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
            _view.Anonymous.Returns(false);

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
            _view.Anonymous.Returns(false);

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
            _view.Anonymous.Returns(false);

            _jira.LoginAsync(Arg.Any<Uri>(), Arg.Any<string>(), Arg.Any<string>())
                .Throws(new HttpRequestException());

            //

            _view.SaveButtonClicked += Raise.Event();

            //

            _view.Received().ShowError(Arg.Any<string>());
            _view.DidNotReceive().Close();
            _preferences.DidNotReceive().SetPreferences(Arg.Any<Preferences>());
        }

        [Test]
        public void DoesNotLoginToJiraIfAnonymousSet() {
            _preferences.GetPreferences().Returns(new Preferences());
            _presenter.Initialize(_view);

            _view.DashboardUrl.Returns("http://www.google.com/somewhere");
            _view.Anonymous.Returns(true);

            //

            _view.SaveButtonClicked += Raise.Event();

            //

            _jira.DidNotReceive().LoginAsync(Arg.Any<Uri>(), Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void SetsAnonymousIfThereAreNoCookiesInPreferences() {
            _preferences.GetPreferences().Returns(new Preferences());

            //

            _presenter.Initialize(_view);

            //

            _view.Received().Anonymous = true;
        }

        [Test]
        public void SetsLoginUsernameIfThereIsOne() {
            _preferences.GetPreferences().Returns(new Preferences { LoginUsername = "user" });

            //

            _presenter.Initialize(_view);

            //

            _view.Received().LoginUsername = "user";
        }

        [Test]
        public void SetsJiraUrlIfThereIsOne() {
            _preferences.GetPreferences()
                .Returns(new Preferences {DashboardUri = new Uri("http://somejira.atlassian.net/some/dashboard/url")});

            //

            _presenter.Initialize(_view);

            //

            _view.Received().JiraUrl = "http://somejira.atlassian.net/";
        }

        [Test]
        public void DoesNotShowErrorsForMissingUsernamePasswordIfAnonymousIsSet() {
            _preferences.GetPreferences().Returns(new Preferences());
            _presenter.Initialize(_view);

            _view.DashboardUrl.Returns("http://www.google.com/somewhere");
            _view.LoginUsername.Returns(string.Empty);
            _view.LoginPassword.Returns(string.Empty);
            _view.Anonymous.Returns(true);

            //

            _view.SaveButtonClicked += Raise.Event();

            //

            _view.DidNotReceive().ShowError(Arg.Any<string>());
            _view.Received().Close();
            _preferences.Received().SetPreferences(Arg.Any<Preferences>());
        }
    }
}