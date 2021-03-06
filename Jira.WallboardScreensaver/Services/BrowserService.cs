﻿using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace Jira.WallboardScreensaver.Services {
    public interface IBrowserService {
        void SetCookie(Uri baseUri, string name, string value);
        void ConfigureEmulation();
    }

    public class BrowserService : IBrowserService {
        public virtual void SetCookie(Uri baseUri, string name, string value) {
            InternetSetCookie(baseUri.ToString(), name, value);
        }

        public virtual void ConfigureEmulation() {
            var exeName = Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);
            Registry.SetValue(
                @"HKEY_CURRENT_USER\Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION",
                exeName, 0x2AF8, RegistryValueKind.DWord);
        }

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool InternetSetCookie(string lpszUrlName, string lbszCookieName, string lpszCookieData);
    }
}