using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Jira.WallboardScreensaver.Services;
using Microsoft.Win32;
using NUnit.Framework;

namespace Jira.WallboardScreensaver.Tests {
    [TestFixture]
    public class PreferencesServiceTests {
        [SetUp]
        public void SetUp() {
            _softwareKey = Registry.CurrentUser.OpenSubKey("Software", RegistryKeyPermissionCheck.ReadWriteSubTree);

            Debug.Assert(_softwareKey != null);

            _key = _softwareKey.CreateSubKey(TestSubkeyName);
            _service = new PreferencesService(_key);
        }

        [TearDown]
        public void TearDown() {
            _softwareKey.DeleteSubKeyTree(TestSubkeyName);
        }

        private const string TestSubkeyName = "Jira Wallboard Screensaver Tests";

        private RegistryKey _softwareKey;
        private RegistryKey _key;
        private PreferencesService _service;

        [Test]
        public void ReturnsCorrectCookieIfOneIsConfigured() {
            _key.SetValue(PreferencesService.LoginCookiesKey, @"{""cookie"": ""value""}");

            //

            var p = _service.GetPreferences();

            //

            Assert.That(p.LoginCookies.Count, Is.EqualTo(1));
            Assert.That(p.LoginCookies["cookie"], Is.EqualTo("value"));
        }

        [Test]
        public void ReturnsCorrectCookiesIfManyAreConfigured() {
            _key.SetValue(PreferencesService.LoginCookiesKey, @"{
                ""cookie1"": ""value3"",
                ""cookie2"": ""value2"",
                ""cookie3"": ""value1""
            }");

            //

            var p = _service.GetPreferences();

            //

            Assert.That(p.LoginCookies.Count, Is.EqualTo(3));
            Assert.That(p.LoginCookies["cookie1"], Is.EqualTo("value3"));
            Assert.That(p.LoginCookies["cookie2"], Is.EqualTo("value2"));
            Assert.That(p.LoginCookies["cookie3"], Is.EqualTo("value1"));
        }

        [Test]
        public void ReturnsJiraUriIfItIsConfigured() {
            _key.SetValue(PreferencesService.JiraUriKey, "http://www.google.com");

            //

            var p = _service.GetPreferences();

            //

            Assert.That(p.JiraUri, Is.EqualTo(new Uri("http://www.google.com")));
        }

        [Test]
        public void ReturnsLoginUsernameIfItIsConfigured() {
            _key.SetValue(PreferencesService.LoginUsernameKey, "user");

            //

            var p = _service.GetPreferences();

            //

            Assert.That(p.LoginUsername, Is.EqualTo("user"));
        }

        [Test]
        public void ReturnsDashboardIdIfItIsConfigured() {
            _key.SetValue(PreferencesService.DashboardIdKey, 1);

            //

            var p = _service.GetPreferences();

            //

            Assert.That(p.DashboardId, Is.EqualTo(1));
        }

        [Test]
        public void ReturnsEmptyCookieCollectionIfNoneAreConfigured() {
            //

            var p = _service.GetPreferences();

            //

            Assert.That(p.LoginCookies.Count, Is.Zero);
        }

        [Test]
        public void ReturnsNullDashboardIdIfNoneIsConfigured() {
            //

            var p = _service.GetPreferences();

            //

            Assert.That(p.DashboardId, Is.Null);
        }

        [Test]
        public void ReturnsNulJiraUriIfNoneIsConfigured() {
            //

            var p = _service.GetPreferences();

            //

            Assert.That(p.JiraUri, Is.Null);
        }

        [Test]
        public void ReturnsNullLoginUsernameIfNoneIsConfigured() {
            //

            var p = _service.GetPreferences();

            //

            Assert.That(p.LoginUsername, Is.Null);
        }

        [Test]
        public void ThrowsIfCookiesAreNotValid() {
            _key.SetValue(PreferencesService.LoginCookiesKey, "notacookie");

            // //

            Assert.Throws<ArgumentException>(
                () => _service.GetPreferences());
        }

        [Test]
        public void ThrowsIfDashboardUriIsNotValid() {
            _key.SetValue(PreferencesService.JiraUriKey, "aabnn");

            // //

            Assert.Throws<ArgumentException>(
                () => _service.GetPreferences());
        }

        [Test]
        public void SavesJiraUriIfItIsProvided() {
            var p = new Preferences {JiraUri = new Uri("http://www.google.com/")};

            //

            _service.SetPreferences(p);

            //

            Assert.That(_key.GetValue(PreferencesService.JiraUriKey), Is.EqualTo("http://www.google.com/"));
        }

        [Test]
        public void SavesLoginCookiesIfTheyAreProvided() {
            var p = new Preferences {
                LoginCookies = new ReadOnlyDictionary<string, string>(
                    new Dictionary<string, string> {
                        {"cookie1", "value1"},
                        {"cookie2", "value2"}
                    }
                )
            };

            //

            _service.SetPreferences(p);

            //

            Assert.That(_key.GetValue(PreferencesService.LoginCookiesKey), Is.EqualTo(@"{""cookie1"":""value1"",""cookie2"":""value2""}"));
        }

        [Test]
        public void SavesLoginUsernameIfItIsProvided() {
            var p = new Preferences { LoginUsername = "user" };

            //

            _service.SetPreferences(p);

            //

            Assert.That(_key.GetValue(PreferencesService.LoginUsernameKey), Is.EqualTo("user"));
        }

        [Test]
        public void SavesDashboardIdIfItIsProvided() {
            var p = new Preferences { DashboardId = 1 };

            //

            _service.SetPreferences(p);

            //

            Assert.That(_key.GetValue(PreferencesService.DashboardIdKey), Is.EqualTo(1));
        }
    }
}