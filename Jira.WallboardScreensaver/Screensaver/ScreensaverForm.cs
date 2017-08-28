using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jira.WallboardScreensaver.Screensaver;

namespace Jira.WallboardScreensaver.Screensaver {
    public partial class ScreensaverForm : Form, IScreensaverView
    {
        public ScreensaverForm()
        {
            InitializeComponent();
        }
    }
}
