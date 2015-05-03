using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using UtilX;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : MetroWindow
    {
        public Settings()
        {
            InitializeComponent();

            this.toggleStartUp.IsChecked = AppSettings.Default.StartUp;
            this.toggleCpuRamDial.IsChecked = AppSettings.Default.ShowCpuRamDial;
            this.toggleActiveWindowRestriction.IsChecked = AppSettings.Default.RestrictWindow;
            this.txtActiveWindowCount.Value = AppSettings.Default.ActiveExplorerThreshold;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtActiveWindowCount_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            AppSettings.Default.ActiveExplorerThreshold = Convert.ToInt16(this.txtActiveWindowCount.Value);
        }

        private void toggleStartUp_IsCheckedChanged(object sender, EventArgs e)
        {
            AppSettings.Default.StartUp = (bool)this.toggleStartUp.IsChecked;
        }

        private void toggleCpuRamDial_IsCheckedChanged(object sender, EventArgs e)
        {
            AppSettings.Default.ShowCpuRamDial = (bool)this.toggleCpuRamDial.IsChecked;
        }

        private void toggleActiveWindowRestriction_IsCheckedChanged(object sender, EventArgs e)
        {
            AppSettings.Default.RestrictWindow = (bool)this.toggleActiveWindowRestriction.IsChecked;
        }
    }
}
