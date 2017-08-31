using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jira.WallboardScreensaver.EditPreferences;
using Microsoft.Win32;
using NSubstitute;
using NUnit.Framework;

namespace Jira.WallboardScreensaver.Tests {
    [TestFixture]
    public class EditPreferencesPresenterTests
    {
        private EditPreferencesPresenter _presenter;
        private IEditPreferencesView _view;
        private IPreferencesService _preferences;

        [SetUp]
        public void SetUp()
        {
            _preferences = Substitute.For<IPreferencesService>();
            _view = Substitute.For<IEditPreferencesView>();
            _presenter = new EditPreferencesPresenter(_preferences);
        }

        [Test]
        public void LoadsPreferencesIntoView()
        {
            var p = new Preferences
            {
                DashboardUri = new Uri("http://www.google.com/"),
                LoginCookies = new Dictionary<string, string>
                {
                    { "cookie1", "value1" },
                    { "cookie2", "value2" }
                }
            };

            _preferences.GetPreferences().Returns(p);

            //

            _presenter.Initialize(_view);

            //

            _preferences.Received().GetPreferences();
            _view.Received().DashboardUrl = "http://www.google.com/";
            _view.Received().LoginCookies = "cookie1=value1;cookie2=value2";
        }

        [Test]
        public void SavesPreferencesFromViewWhenSaveButtonClicked()
        {
            _preferences.GetPreferences().Returns(new Preferences());
            _presenter.Initialize(_view);
            _view.DashboardUrl.Returns("http://www.google.com/");
            _view.LoginCookies.Returns("cookie1=value1;cookie2=value2");

            //

            _view.SaveButtonClicked += Raise.Event();

            //

            _preferences.Received().SetPreferences(Arg.Do<Preferences>(p =>
            {
                Assert.That(p.DashboardUri, Is.EqualTo(new Uri("http://www.google.com/")));
                Assert.That(p.LoginCookies, Is.EquivalentTo(new []
                {
                    new KeyValuePair<string, string>("cookie1", "value1"), 
                }));
            }));
        }

        [Test]
        public void DoesNotSavePreferencesWhenCancelButtonClicked()
        {
            _preferences.GetPreferences().Returns(new Preferences());

            //

            _presenter.Initialize(_view);
            _view.CancelButtonClicked += Raise.Event();

            //

            _preferences.DidNotReceive().SetPreferences(Arg.Any<Preferences>());
        }
    }
}
