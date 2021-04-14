using FlightSimulatorApp.ViewModels;
using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Controls;

/***
 * Anomalies- View class, holds a VM to sends command and bind data with its properties.
 * ***/

namespace FlightSimulatorApp.Views
{
    /// <summary>
    /// Interaction logic for Anomalies.xaml
    /// </summary>
    public partial class Anomalies : MetroWindow
    {

        private ViewModelAnomalies vm;

        public Anomalies()
        {
            InitializeComponent();
            this.vm = new ViewModelAnomalies(MainWindow.getModel());
            DataContext = vm;
        }



        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var selectedItem in MyListBox.SelectedItems)
            {
                string feature = selectedItem.ToString();
                vm.updateSelectedAnomalyFeature(feature);
            }
        }

        private void Anomalies_Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.vm.LoadDLL();
        }

        private void AnomalousPointsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var selectedItem in AnomalousPointsListBox.SelectedItems)
            {
                string timestep = (string)selectedItem;

                this.vm.changeTimestepByString(timestep);
            }
        }
    }
}