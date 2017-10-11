using System;
using System.Collections.Generic;

namespace Jira.WallboardScreensaver {
    public class Preferences {
        public const string DashboardRelativeUrlFormat = "plugins/servlet/Wallboard/?dashboardId={0}";

        public Preferences() {
            // ReSharper disable once VirtualMemberCallInConstructor
            LoginCookies = new Dictionary<string, string>();
        }

        public virtual string LoginUsername { get; set; }
        public virtual Uri JiraUri { get; set; }
        public virtual IReadOnlyDictionary<string, string> LoginCookies { get; set; }
        public virtual int? DashboardId { get; set; }

        public virtual Uri GetDashboardUri() {
            if (DashboardId == null || JiraUri == null) {
                return null;
            }

            return new Uri(JiraUri, string.Format(DashboardRelativeUrlFormat, DashboardId));
        }
    }
}