using System;
using System.Threading.Tasks;

namespace Jira.WallboardScreensaver {
    public interface ITaskService
    {
        Task Delay(TimeSpan delay);
    }

    public class TaskService : ITaskService
    {
        public virtual Task Delay(TimeSpan delay)
        {
            return Task.Delay(delay);
        }
    }
}
