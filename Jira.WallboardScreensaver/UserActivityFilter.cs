using System;
using System.Windows.Forms;
using Timer = System.Threading.Timer;

namespace Jira.WallboardScreensaver {
    public class UserActivityFilter : IMessageFilter {
        public TimeSpan IdleTimeout { get; set; }

        public virtual event EventHandler<EventArgs> UserActive;
        public virtual event EventHandler<EventArgs> UserIdle;

        public UserActivityFilter()
        {
            _timer = new Timer(FireIdleEvent);
        }

        // ReSharper disable once InconsistentNaming
        private const int WM_MOUSEMOVE = 0x0200;
        // ReSharper disable once InconsistentNaming
        private const int WM_MBUTTONDBLCLK = 0x209;
        // ReSharper disable once InconsistentNaming
        private const int WM_KEYDOWN = 0x100;
        // ReSharper disable once InconsistentNaming
        private const int WM_KEYUP = 0x101;

        private readonly Timer _timer;

        public bool PreFilterMessage(ref Message m) {
            if ((m.Msg >= WM_MOUSEMOVE && m.Msg <= WM_MBUTTONDBLCLK)
                || m.Msg == WM_KEYDOWN
                || m.Msg == WM_KEYUP)
            {
                UserActive?.Invoke(this, EventArgs.Empty);

                if (IdleTimeout != default(TimeSpan))
                {
                    _timer.Change((int) IdleTimeout.TotalMilliseconds, -1);
                }
            }
            // Always allow message to continue to the next filter control
            return false;
        }

        private void FireIdleEvent(object state)
        {
            UserIdle?.Invoke(this, EventArgs.Empty);
        }
    }
}