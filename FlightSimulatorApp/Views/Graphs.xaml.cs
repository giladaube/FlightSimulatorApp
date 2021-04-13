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
using MahApps.Metro.Controls;

namespace FlightSimulatorApp.Views
{
    /// <summary>
    /// Interaction logic for Graphs.xaml
    /// </summary>
    partial class Graphs : MetroWindow
    {
        private ViewModelGraphs vm;
        private bool wasDLLSelected = false;
        private string dllPath;


        public Graphs()
        {
            InitializeComponent();
            this.vm = new ViewModelGraphs(MainWindow.getModel());
            this.DataContext = this.vm;

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var selectedItem in MyListBox.SelectedItems)
            { 
                string feature = selectedItem.ToString();
                vm.updateSelectedFeature(feature);
            }
        }

        private void Graph_Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.vm.LearnNormal();
            vm.setGraphWindowOpened(true);
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
                vm.setDllPath(dllPath);
                Anomalies AnomaliesWindow = new Anomalies();
                AnomaliesWindow.Show();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            vm.setGraphWindowOpened(false);
            MainWindow.isGraphOpen = false;
        }
    }
}
