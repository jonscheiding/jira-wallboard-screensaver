using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jira.WallboardScreensaver {
    public class Preferences {
        public Preferences()
        {
            LoginCookies = new Dictionary<string, string>();
        }

        public virtual Uri DashboardUri { get; set; }
        public virtual IReadOnlyDictionary<string, string> LoginCookies { get; set; }
    }
}
