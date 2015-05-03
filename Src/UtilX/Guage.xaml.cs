using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Expression.Shapes;

namespace UtilX
{
	/// <summary>
	/// Interaction logic for Guage.xaml
	/// </summary>
	public partial class Guage : UserControl
	{
        DoubleAnimation myDoubleAnimation = new DoubleAnimation();

		public Guage()
		{
			this.InitializeComponent();

            
		}

	    public void SetValue(double value)
	    {
            myDoubleAnimation.To = value * 3.6;

	        this.Percentage.Content = Math.Round(value, 0).ToString() + " %";

            this.Progress.BeginAnimation(Arc.EndAngleProperty, myDoubleAnimation); 
	    }

	    public void SetSource(string source)
	    {
	        this.Icon.Source = new BitmapImage(new Uri(source, UriKind.Relative));
	    }
	}
}