using System;
using System.Windows.Forms;

namespace Jira.WallboardScreensaver.Services {
    public interface IErrorMessageService {
        void ShowErrorMessage(object view, string message, string title);
    }

    public class ErrorMessageService : IErrorMessageService {
        class Owner : IWin32Window {
            public IntPtr Handle { get; set; }
        }

        public void ShowErrorMessage(object view, string message, string title) {
            var form = view as Form;

            if (form != null) {
                MessageBox.Show(new Owner {Handle = form.Handle},
                    message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else {
                MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}