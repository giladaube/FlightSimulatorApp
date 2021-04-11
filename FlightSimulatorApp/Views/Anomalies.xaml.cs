using FlightSimulatorApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FlightSimulatorApp.Views
{
    /// <summary>
    /// Interaction logic for Anomalies.xaml
    /// </summary>
    public partial class Anomalies : Window
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

        }
    }
}
