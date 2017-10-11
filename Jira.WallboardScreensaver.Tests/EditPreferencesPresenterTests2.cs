using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Jira.WallboardScreensaver.EditPreferences2;
using Jira.WallboardScreensaver.Services;
using NSubstitute;
using NUnit.Framework;
using JiraCredentials = System.Collections.Generic.IReadOnlyDictionary<string, string>;

namespace Jira.WallboardScreensaver.Tests {
    [TestFixture]
    public class EditPreferencesPresenterTests2 {
        private EditPreferencesPresenter _presenter;
        private IEditPreferencesView _view;
        private IChildPresenter<IJiraLoginView, IJiraLoginParent> _childPresenter;
        private IJiraLoginView _childView;
        private IPreferencesService _preferences;
        private IJiraService _jira;
        private IErrorMessageService _errors;

        [SetUp]
        public void SetUp() {
            _view = Substitute.For<IEditPreferencesView>();
            _childPresenter = Substitute.For<IChildPresenter<IJiraLoginView, IJiraLoginParent>>();
            _childView = Substitute.For<IJiraLoginView>();
            _preferences = Substitute.For<IPreferencesService>();
            _jira = Substitute.For<IJiraService>();
            _errors = TestHelper.LogErrors(Substitute.For<IErrorMessageService>());

            _presenter = new EditPreferencesPresenter(_childPresenter, _preferences, _jira, _errors);

            _view.CreateJiraLoginView().Returns(_childView);
        }

        [Test]
        public void PresentsJiraLoginViewWhenLoginButtonClicked() {
            _presenter.Initialize(_view);
            _view.CreateJiraLoginView().Returns(_childView);

            //

            _view.JiraLoginButtonClicked += Raise.Event();

            //

            _childPresenter.Received().Initialize(_childView, Arg.Any<IJiraLoginParent>());
        }

        [Test]
        public void PassesJiraUrlToJiraLoginView() {
            _presenter.Initialize(_view);

            _view.JiraUrl = "http://somejira.atlassian.net";

            //

            _view.JiraLoginButtonClicked += Raise.Event();

            //

            _childPresenter.Received()
                .Initialize(_childView, Arg.Is<IJiraLoginParent>(
                    arg => arg.JiraUrl == "http://somejira.atlassian.net"));
        }

        [Test]
        public void LoadsPreferencesWhenViewIsInitialized() {
            //

            _presenter.Initialize(_view);

            //

            _preferences.Received().GetPreferences();
        }

        [Test]
        public void PassesUsernameFromPreferencesToJiraLoginView() {
            _preferences.GetPreferences().Returns(new Preferences {LoginUsername = "username"});
            _presenter.Initialize(_view);

            //

            _view.JiraLoginButtonClicked += Raise.Event();

            //

            _childPresenter.Received()
                .Initialize(_childView, Arg.Is<IJiraLoginParent>(
                    arg => arg.Username == "username"));
        }

        [Test]
        public void LoadsJiraDashboardsWhenLoadButtonClicked() {
            _presenter.Initialize(_view);
            _view.JiraUrl = "http://somejira.atlassian.net";

            //

            _view.LoadDashboardsButtonClicked += Raise.Event();

            //

            _jira.Received().GetDashboardsAsync(Arg.Any<Uri>(), Arg.Any<JiraCredentials>());
        }

        [Test]
        public void ShowsErrorIfLoadDashboardsButtonClickedWithInvalidJiraUrl() {
            _presenter.Initialize(_view);
            _view.JiraUrl = "bad";

            //

            _view.LoadDashboardsButtonClicked += Raise.Event();

            //

            _errors.Received().ShowErrorMessage(_view, Arg.Any<string>(), Arg.Any<string>());
            _preferences.DidNotReceive().SetPreferences(Arg.Any<Preferences>());
        }

        [Test]
        public void UsesCredentialsFromPreferencesWhenLoadingDashboard() {
            var credentials = Substitute.For<JiraCredentials>();

            _preferences.GetPreferences()
                .Returns(new Preferences {
                    LoginCookies = credentials
                });

            _view.JiraUrl = "http://somejira.atlassian.net";

            _presenter.Initialize(_view);

            //

            _view.LoadDashboardsButtonClicked += Raise.Event();

            //

            _jira.Received().GetDashboardsAsync(Arg.Any<Uri>(), credentials);
        }

        [Test]
        public void UsesCredentialsFromJiraLoginViewIfTheyHaveBeenUpdated() {
            var credentials = Substitute.For<JiraCredentials>();

            _view.JiraUrl = "http://somejira.atlassian.net";

            _presenter.Initialize(_view);

            _childPresenter.When(c => c.Initialize(_childView, Arg.Any<IJiraLoginParent>()))
                .Do(c => c.Arg<IJiraLoginParent>().UpdateJiraCredentials(credentials));

            //

            _view.JiraLoginButtonClicked += Raise.Event();
            _view.LoadDashboardsButtonClicked += Raise.Event();

            //

            _jira.Received().GetDashboardsAsync(Arg.Any<Uri>(), credentials);
        }

        [Test]
        public void UsesJiraUriFromViewToGetDashboards() {
            _presenter.Initialize(_view);
            _view.JiraUrl = "http://somejira.atlassian.net";

            //

            _view.LoadDashboardsButtonClicked += Raise.Event();

            //

            _jira.Received().GetDashboardsAsync(new Uri("http://somejira.atlassian.net"), Arg.Any<JiraCredentials>());
        }

        [Test]
        public void SavesPreferencesWhenSaveButtonClicked() {
            var preferences = new Preferences();
            _preferences.GetPreferences().Returns(preferences);
            _presenter.Initialize(_view);
            _view.JiraUrl = "http://somejira.atlassian.net";
            _view.SelectedDashboardItem = new JiraDashboard("", 1);

            //

            _view.SaveButtonClicked += Raise.Event();

            //

            _preferences.Received().SetPreferences(preferences);
        }

        [Test]
        public void SavesUpdatedJiraUrlInPreferences() {
            _presenter.Initialize(_view);
            _view.JiraUrl = "http://somejira.atlassian.net";
            _view.SelectedDashboardItem = new JiraDashboard("", 1);

            //

            _view.SaveButtonClicked += Raise.Event();

            //

            _preferences.Received()
                .SetPreferences(Arg.Is<Preferences>(
                    p => p.JiraUri == new Uri("http://somejira.atlassian.net")));
        }

        [Test]
        public void SavesUpdatedCredentialsInPreferences() {
            var credentials = Substitute.For<JiraCredentials>();

            _view.JiraUrl = "http://somejira.atlassian.net";
            _view.SelectedDashboardItem = new JiraDashboard("", 1);

            _presenter.Initialize(_view);

            _childPresenter.When(c => c.Initialize(_childView, Arg.Any<IJiraLoginParent>()))
                .Do(c => c.Arg<IJiraLoginParent>().UpdateJiraCredentials(credentials));

            //

            _view.JiraLoginButtonClicked += Raise.Event();
            _view.SaveButtonClicked += Raise.Event();

            //

            _preferences.Received()
                .SetPreferences(Arg.Is<Preferences>(
                    p => p.LoginCookies == credentials));
        }

        [Test]
        public void ShowsErrorIfSaveButtonClickedWithInvalidJiraUrl() {
            _presenter.Initialize(_view);
            _view.JiraUrl = "bad";

            //

            _view.SaveButtonClicked += Raise.Event();

            //

            _errors.Received().ShowErrorMessage(_view, Arg.Any<string>(), Arg.Any<string>());
            _preferences.DidNotReceive().SetPreferences(Arg.Any<Preferences>());
            _view.DidNotReceive().Close();
        }

        [Test]
        public void ShowsErrorIfDashboardsFailToLoad() {
            _presenter.Initialize(_view);
            _view.JiraUrl = "http://somejira.atlassian.net";
            _jira.GetDashboardsAsync(Arg.Any<Uri>(), Arg.Any<JiraCredentials>())
                .Returns(Task.Run((Func<IEnumerable<JiraDashboard>>)(() => throw new HttpRequestException())));

            //

            _view.LoadDashboardsButtonClicked += Raise.Event();

            //

            Thread.Sleep(100);

            _errors.Received().ShowErrorMessage(_view, Arg.Any<string>(), Arg.Any<string>());
            _preferences.DidNotReceive().SetPreferences(Arg.Any<Preferences>());
        }

        [Test]
        public void PutsDashboardsIntoListViewWhenTheyAreLoaded() {
            var dashboards = new[] {
                new JiraDashboard("Dashboard 1000", 1000),
                new JiraDashboard("Dashboard 2000", 2000)
            };

            _presenter.Initialize(_view);
            _view.JiraUrl = "http://somejira.atlassian.net";
            _jira.GetDashboardsAsync(Arg.Any<Uri>(), Arg.Any<JiraCredentials>())
                .Returns(Task.FromResult(dashboards.AsEnumerable()));

            //

            _view.LoadDashboardsButtonClicked += Raise.Event();

            //

            _view.Received().DashboardItems = Arg.Do<IDashboardDisplayItem[]>(
                a => Assert.That(a, Is.EquivalentTo(dashboards)));
        }

        [Test]
        public void ShowsErrorIfSavingWithoutSelectedDashboard() {
            _presenter.Initialize(_view);
            _view.SelectedDashboardItem = null;
            _view.JiraUrl = "http://somejira.atlassian.net";

            //

            _view.SaveButtonClicked += Raise.Event();

            //

            _errors.Received().ShowErrorMessage(_view, Arg.Any<string>(), Arg.Any<string>());
            _preferences.DidNotReceive().SetPreferences(Arg.Any<Preferences>());
            _view.DidNotReceive().Close();
        }

        [Test]
        public void SavesSelectedDashboardIdIntoPreferences() {
            _presenter.Initialize(_view);
            _view.SelectedDashboardItem = new JiraDashboard("Dashboard 1000", 1000);
            _view.JiraUrl = "http://somejira.atlassian.net";

            //

            _view.SaveButtonClicked += Raise.Event();

            //

            _preferences.Received().SetPreferences(Arg.Is<Preferences>(
                p => p.DashboardId == 1000));
        }

        [Test]
        public void ClosesFormWhenValidPreferencesAreSaved() {
            _presenter.Initialize(_view);
            _view.SelectedDashboardItem = new JiraDashboard("Dashboard 1000", 1000);
            _view.JiraUrl = "http://somejira.atlassian.net";

            //

            _view.SaveButtonClicked += Raise.Event();

            //

            _view.Received().Close();
        }
    }
}