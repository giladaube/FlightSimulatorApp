using FlightSimulatorApp.ViewModels;
using System.Windows.Controls;

/***
 * RPYView- View class, holds a VM to sends command and bind data with its properties.
 * ***/

namespace FlightSimulatorApp.Views
{
    /// <summary>
    /// Interaction logic for RPYView.xaml
    /// </summary>
    public partial class RPYView : UserControl
    {
        private ViewModelControls vm;
        public RPYView()
        {
            InitializeComponent();
            this.vm = new ViewModelControls(MainWindow.getModel());
            this.DataContext = this.vm;
        }
    }
}
