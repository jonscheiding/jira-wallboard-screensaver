using System;
using System.Collections.Generic;

namespace Jira.WallboardScreensaver
{
    public class ConfigurationService
    {
        public virtual Uri DashboardUri => new Uri("http://www.google.com");
        public virtual KeyValuePair<string, string> LoginCookie => new KeyValuePair<string, string>("", "");
    }
}