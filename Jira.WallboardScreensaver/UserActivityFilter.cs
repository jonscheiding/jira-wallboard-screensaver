using System;
using System.Threading;
using System.Windows.Forms;

namespace Jira.WallboardScreensaver
{
    public class UserActivityFilter : IMessageFilter {
        public delegate void UserActivityEvent();

        private const int WM_MOUSEMOVE = 0x0200;
        private const int WM_MBUTTONDBLCLK = 0x209;
        private const int WM_KEYDOWN = 0x100;
        private const int WM_KEYUP = 0x101;

        public event UserActivityEvent UserActivity;

        public bool PreFilterMessage(ref Message m) {
            if ((m.Msg >= WM_MOUSEMOVE && m.Msg <= WM_MBUTTONDBLCLK)
                || m.Msg == WM_KEYDOWN
                || m.Msg == WM_KEYUP) {

                if (UserActivity != null)
                {
                    UserActivity.Invoke();
                }
            }
            // Always allow message to continue to the next filter control
            return false;
        }
    }
}