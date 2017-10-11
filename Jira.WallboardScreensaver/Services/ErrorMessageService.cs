using System;
using System.Windows.Forms;

namespace Jira.WallboardScreensaver.Services {
    public interface IErrorMessageService {
        void ShowErrorMessage(object view, string message, string title);
    }

    public class ErrorMessageService : IErrorMessageService {
        public void ShowErrorMessage(object view, string message, string title) {
            throw new NotImplementedException();
        }
    }
}