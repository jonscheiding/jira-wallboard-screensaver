using System;
using System.Threading.Tasks;

namespace Jira.WallboardScreensaver {
    public class TaskService {
        public virtual Task Delay(TimeSpan delay)
        {
            return Task.Delay(delay);
        }
    }
}
