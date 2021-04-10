using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FlightSimulatorApp.Models;
using FlightSimulatorApp.ViewModels;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FlightSimulatorApp.Views
{
    /// <summary>
    /// Interaction logic for Graphs.xaml
    /// </summary>
    partial class Graphs : Window
    {
        private GraphsViewModel vm;
        private bool wasDLLSelected = false;
        private string dllPath;

        public GraphsViewModel Vm
        {
            get { return this.vm; }
            set { this.vm = value; }
        }

        public Graphs(GraphsViewModel vm)
        {
            InitializeComponent();
            this.Vm = vm;
            DataContext = vm;

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var selectedItem in MyListBox.SelectedItems)
            {
                string feature = selectedItem.ToString();
                vm.updateSelectedFeature(feature);
                vm.setGraphWindowOpened(true);
            }
        }

        private void Graph_Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.vm.LearnNormal();
        }

        private void DLL_Button_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".dll";
            dlg.Filter = "DLL files (*.dll)|*.dll|All files (*.*)|*.*";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                dllPath = dlg.FileName;
                wasDLLSelected = true;
                vm.setAnomaliesWindowOpened(true);
                vm.setDllPath(dllPath);
                //Anomalies AnomaliesWindow = new Anomalies(vm);
                //AnomaliesWindow.Show();
            }
        }
    }
}
