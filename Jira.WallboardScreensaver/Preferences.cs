using System;
using System.Collections.Generic;

namespace Jira.WallboardScreensaver {
    public class Preferences {
        public Preferences() {
            // ReSharper disable once VirtualMemberCallInConstructor
            LoginCookies = new Dictionary<string, string>();
        }

        public virtual Uri DashboardUri { get; set; }
        public virtual IReadOnlyDictionary<string, string> LoginCookies { get; set; }
    }
}