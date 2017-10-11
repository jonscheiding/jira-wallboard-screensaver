using System;
using System.Collections.Generic;

namespace Jira.WallboardScreensaver {
    public class Preferences {
        public Preferences() {
            // ReSharper disable once VirtualMemberCallInConstructor
            LoginCookies = new Dictionary<string, string>();
        }

        public virtual string LoginUsername { get; set; }
        public virtual Uri DashboardUri { get; set; }
        public virtual Uri JiraUri { get; set; }
        public virtual IReadOnlyDictionary<string, string> LoginCookies { get; set; }
        public virtual int? DashboardId { get; set; }
    }
}