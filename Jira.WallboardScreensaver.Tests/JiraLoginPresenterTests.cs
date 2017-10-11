using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Jira.WallboardScreensaver.EditPreferences2;
using Jira.WallboardScreensaver.Services;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;

namespace Jira.WallboardScreensaver.Tests {
    [TestFixture]
    public class JiraLoginPresenterTests {
        private JiraLoginPresenter _presenter;

        private IJiraService _jiraService;

        private IJiraLoginView _view;
        private IJiraLoginParent _parent;

        [SetUp]
        public void SetUp() {
            _jiraService = Substitute.For<IJiraService>();

            _view = Substitute.For<IJiraLoginView>();
            _parent = Substitute.For<IJiraLoginParent>();
            _presenter = new JiraLoginPresenter(_jiraService);
        }

        [Test]
        public void ShowsErrorWhenLoginButtonClickedIfUsernameNotProvided() {
            _presenter.Initialize(_view, _parent);
            _view.Username = "";
            _view.Password = "password";
            
            //

            _view.LoginButtonClicked += Raise.Event();

            //

            _view.Received().ShowError(Arg.Any<string>());
        }

        [Test]
        public void ShowsErrorWhenLoginButtonClickedIfPasswordNotProvided() {
            _presenter.Initialize(_view, _parent);
            _view.Username = "username";
            _view.Password = "";

            //

            _view.LoginButtonClicked += Raise.Event();

            //

            _view.Received().ShowError(Arg.Any<string>());
        }

        [Test]
        public void LogsInToJiraWithProvidedUrlAndEnteredCredentialsWhenLoginButtonClicked() {
            _presenter.Initialize(_view, _parent);

            _view.Username = "username";
            _view.Password = "password";
            _parent.JiraUrl.Returns("http://somejira.atlassian.net");

            //

            _view.LoginButtonClicked += Raise.Event();

            //

            _jiraService.Received().LoginAsync(new Uri("http://somejira.atlassian.net"), "username", "password");
        }

        [Test]
        public void ShowsErrorIfJiraLoginFails() {
            _jiraService.LoginAsync(Arg.Any<Uri>(), Arg.Any<string>(), Arg.Any<string>())
                .Throws(new HttpRequestException());

            _presenter.Initialize(_view, _parent);

            _view.Username = "username";
            _view.Password = "password";
            _parent.JiraUrl.Returns("http://somejira.atlassian.net");

            //

            _view.LoginButtonClicked += Raise.Event();

            //

            _jiraService.Received().LoginAsync(Arg.Any<Uri>(), Arg.Any<string>(), Arg.Any<string>());
            _view.Received().ShowError(Arg.Any<string>());
        }

        [Test]
        public void ClosesFormIfJiraLoginSucceeds() {
            _presenter.Initialize(_view, _parent);

            _view.Username = "username";
            _view.Password = "password";
            _parent.JiraUrl.Returns("http://somejira.atlassian.net");

            //

            _view.LoginButtonClicked += Raise.Event();

            //

            _view.Received().Close();
        }

        [Test]
        public void UpdatesJiraCredentialsIfJiraLoginSucceeds() {
            var credentials = Substitute.For<IReadOnlyDictionary<string, string>>();

            _presenter.Initialize(_view, _parent);

            _jiraService.LoginAsync(Arg.Any<Uri>(), Arg.Any<string>(), Arg.Any<string>())
                .Returns(Task.FromResult(credentials));

            _view.Username = "username";
            _view.Password = "password";
            _parent.JiraUrl.Returns("http://somejira.atlassian.net");

            //

            _view.LoginButtonClicked += Raise.Event();

            //

            _parent.Received().UpdateJiraCredentials(credentials);
        }

        [Test]
        public void ClosesFormWhenCancelButtonClicked() {
            _presenter.Initialize(_view, _parent);

            //

            _view.CancelButtonClicked += Raise.Event();

            //

            _view.Received().Close();
        }

        [Test]
        public void ClearsJiraCredentialsWhenClearButtonClicked() {
            _presenter.Initialize(_view, _parent);

            //

            _view.ClearButtonClicked += Raise.Event();

            //

            _parent.Received().ClearJiraCredentials();
        }

        [Test]
        public void ClosesFormWhenClearButtonClicked() {
            _presenter.Initialize(_view, _parent);

            //

            _view.ClearButtonClicked += Raise.Event();

            //

            _view.Received().Close();
        }

        [Test]
        public void SetsUsernameIntoViewIfParentProvidesIt() {
            _parent.Username = "username";

            //

            _presenter.Initialize(_view, _parent);

            //

            _view.Received().Username = "username";
        }
    }
}