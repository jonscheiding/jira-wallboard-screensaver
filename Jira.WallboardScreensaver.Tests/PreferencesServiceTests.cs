using System;
using System.Diagnostics;
using Microsoft.Win32;
using NUnit.Framework;

namespace Jira.WallboardScreensaver.Tests {
    [TestFixture]
    public class PreferencesServiceTests
    {
        private const string TestSubkeyName = "Jira Wallboard Screensaver Tests";

        private RegistryKey _softwareKey;
        private RegistryKey _key;
        private PreferencesService _service;

        [SetUp]
        public void SetUp()
        {
            _softwareKey = Registry.CurrentUser.OpenSubKey("Software", RegistryKeyPermissionCheck.ReadWriteSubTree);

            Debug.Assert(_softwareKey != null);

            _key = _softwareKey.CreateSubKey(TestSubkeyName);
            _service = new PreferencesService(_key);
        }

        [Test]
        public void ReturnsNullDashboardUriIfNoneIsConfigured()
        {
            //

            var p = _service.GetPreferences();

            //

            Assert.That(p.DashboardUri, Is.Null);
        }

        [Test]
        public void ReturnsDashboardUriIfItIsConfigured()
        {
            _key.SetValue(PreferencesService.DashboardUriKey, "http://www.google.com");

            //

            var p = _service.GetPreferences();

            //

            Assert.That(p.DashboardUri, Is.EqualTo(new Uri("http://www.google.com")));
        }

        [Test]
        public void ThrowsIfDashboardUriIsNotValid()
        {
            _key.SetValue(PreferencesService.DashboardUriKey, "aabnn");

            // //

            Assert.Throws<ArgumentException>(
                () => _service.GetPreferences());
        }

        [Test]
        public void ReturnsEmptyCookieCollectionIfNoneAreConfigured()
        {
            //

            var p = _service.GetPreferences();

            //

            Assert.That(p.LoginCookies.Count, Is.Zero);
        }

        [Test]
        public void ReturnsCorrectCookieIfOneIsConfigured()
        {
            var cookie = $"cookie{PreferencesService.CookieSeparator}value";
            _key.SetValue(PreferencesService.LoginCookiesKey, new[] {cookie});

            //

            var p = _service.GetPreferences();

            //

            Assert.That(p.LoginCookies.Count, Is.EqualTo(1));
            Assert.That(p.LoginCookies["cookie"], Is.EqualTo("value"));
        }

        [Test]
        public void ReturnsCorrectCookiesIfManyAreConfigured() {
            var cookie1 = $"cookie1{PreferencesService.CookieSeparator}value3";
            var cookie2 = $"cookie2{PreferencesService.CookieSeparator}value2";
            var cookie3 = $"cookie3{PreferencesService.CookieSeparator}value1";
            _key.SetValue(PreferencesService.LoginCookiesKey, new[] { cookie1, cookie2, cookie3 });

            //

            var p = _service.GetPreferences();

            //

            Assert.That(p.LoginCookies.Count, Is.EqualTo(3));
            Assert.That(p.LoginCookies["cookie1"], Is.EqualTo("value3"));
            Assert.That(p.LoginCookies["cookie2"], Is.EqualTo("value2"));
            Assert.That(p.LoginCookies["cookie3"], Is.EqualTo("value1"));
        }

        [Test]
        public void ThrowsIfCookiesAreNotValid()
        {
            _key.SetValue(PreferencesService.LoginCookiesKey, new[] {"notacookie"});

            // //

            Assert.Throws<ArgumentException>(
                () => _service.GetPreferences());
        }

        [TearDown]
        public void TearDown()
        {
            _softwareKey.DeleteSubKeyTree(TestSubkeyName);
        }
    }
}
