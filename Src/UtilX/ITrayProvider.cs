using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WpfApplication1;
using Application = System.Windows.Application;

namespace UtilX
{
    interface ITrayProvider
    {
        void SetUp(string trayIcon);
    }

    class TrayProvider : ITrayProvider
    {
        private NotifyIcon _trayIcon;

        public TrayProvider()
        {
            _trayIcon = new NotifyIcon();
        }

        public void SetUp(string trayIcon)
        {
            this._trayIcon.Icon = new Icon(trayIcon);
            _trayIcon.Visible = true;

            this._trayIcon.ContextMenu = new ContextMenu(new MenuItem[]
            {
                new MenuItem("Settings", OnSettings),
                new MenuItem("Exit", OnExit)
                
            });
        }

        public void OnExit(object obj, EventArgs args)
        {
            Application.Current.Shutdown();
        }

        public void OnSettings(object obj, EventArgs args)
        {
            new Settings().Show();
        }
    }
}
