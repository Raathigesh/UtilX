using System;
using System.Windows;
using System.Windows.Input;

namespace UtilX
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _explorerRestrictionTheshold = 0;
        private bool _doRestrictExplorer  = false;
        private bool _showDial = true;

        private readonly ITrayProvider _trayProvider = new TrayProvider();
        private readonly ISystemInfoProvider _systemInfoProvider = new SystemInfoProvider();
        private readonly IExplorerTerminator _explorerTerminator = new ExplorerTerminator();
        private readonly IStartUpRegister _startUpRegister = new StartUpRegister();
        public MainWindow()
        {
            InitializeComponent();

            this._explorerRestrictionTheshold = AppSettings.Default.ActiveExplorerThreshold;
            this._doRestrictExplorer = AppSettings.Default.RestrictWindow;

            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            // Setup tray icon
            _trayProvider.SetUp(@"Icon/51.ico");
            
            // Position the main frame
            this.Left = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Right - this.Width;
            this.Top = 0;
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;

            //Set dial icons
            this.CpuDial.SetSource("Icon/75124.png");
            this.RamDial.SetSource("Icon/18033.png");

            // Look for app settings change
            AppSettings.Default.PropertyChanged += Default_PropertyChanged;
        }

        void Default_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ActiveExplorerThreshold")
            {
                _explorerRestrictionTheshold = AppSettings.Default.ActiveExplorerThreshold;
            }

            if (e.PropertyName == "StartUp")
            {
                _startUpRegister.RegisterAsStartUp(AppSettings.Default.StartUp);
            }

            if (e.PropertyName == "RestrictWindow")
            {
                this._doRestrictExplorer = AppSettings.Default.RestrictWindow;
            }

            if (e.PropertyName == "ShowCpuRamDial")
            {
                _showDial = AppSettings.Default.ShowCpuRamDial;

                if (_showDial)
                {
                    this.GuageGrid.Children.Add(this.RamDial);
                    this.GuageGrid.Children.Add(this.CpuDial);
                }
                else
                {
                    this.GuageGrid.Children.Remove(this.RamDial);
                    this.GuageGrid.Children.Remove(this.CpuDial);
                }
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (_showDial)
            {
                this.CpuDial.SetValue(_systemInfoProvider.GetCpuUsage());
                this.RamDial.SetValue(_systemInfoProvider.GetRamUsuage());
            }

            if (_doRestrictExplorer)
            {
                _explorerTerminator.Run(_explorerRestrictionTheshold);    
            }
        }
        
        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
