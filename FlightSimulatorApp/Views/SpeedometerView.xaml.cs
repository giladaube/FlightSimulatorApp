using FlightSimulatorApp.ViewModels;
using System.Windows.Controls;

/***
 * SpeedometerView- View class, holds a VM to sends command and bind data with its properties.
 * ***/

namespace FlightSimulatorApp.Views
{
    /// <summary>
    /// Interaction logic for SpeedometerView.xaml
    /// </summary>
    public partial class SpeedometerView : UserControl
    {
        private ViewModelControls vm;
        public SpeedometerView()
        {
            InitializeComponent();
            this.vm = new ViewModelControls(MainWindow.getModel());
            this.DataContext = this.vm;
        }
    }
}
