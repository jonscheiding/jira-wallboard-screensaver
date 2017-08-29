using System;
using System.Collections.Generic;

namespace Jira.WallboardScreensaver
{
    public class ConfigurationService
    {
        public virtual Uri DashboardUri => new Uri("https://www.google.com");

        public virtual Dictionary<string, string> LoginCookies => new Dictionary<string, string>
        {
            { "some-cookie", "some-value" }
        };
    }
}