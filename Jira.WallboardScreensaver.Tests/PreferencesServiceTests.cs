using System;
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
        public void ReturnsDashboardUriIfItIsConfigured() {
            _key.SetValue(PreferencesService.DashboardUriKey, "http://www.google.com");

            //

            var p = _service.GetPreferences();

            //

            Assert.That(p.DashboardUri, Is.EqualTo(new Uri("http://www.google.com")));
        }

        [Test]
        public void ReturnsEmptyCookieCollectionIfNoneAreConfigured() {
            //

            var p = _service.GetPreferences();

            //

            Assert.That(p.LoginCookies.Count, Is.Zero);
        }

        [Test]
        public void ReturnsNullDashboardUriIfNoneIsConfigured() {
            //

            var p = _service.GetPreferences();

            //

            Assert.That(p.DashboardUri, Is.Null);
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
            _key.SetValue(PreferencesService.DashboardUriKey, "aabnn");

            // //

            Assert.Throws<ArgumentException>(
                () => _service.GetPreferences());
        }
    }
}