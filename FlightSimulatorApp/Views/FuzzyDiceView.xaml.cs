using FlightSimulatorApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

/***
 * FuzzyDiceView- View class, holds a VM to sends command and bind data with its properties.
 * ***/

namespace FlightSimulatorApp.Views
{
    /// <summary>
    /// Interaction logic for CompassView.xaml
    /// </summary>
    public partial class FuzzyDiceView : UserControl
    {
        private ViewModelControls vm;
        public FuzzyDiceView()
        {
            InitializeComponent();
            this.vm = new ViewModelControls(MainWindow.getModel());
            this.DataContext = this.vm;
        }
    }

    public class MyConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, 
            object parameter, CultureInfo culture)
        {
            return (float)value / 2;
        }
    

        public object ConvertBack(object value, Type targetType, 
            object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
