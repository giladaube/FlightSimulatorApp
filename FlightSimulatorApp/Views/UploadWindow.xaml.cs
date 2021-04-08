using FlightSimulatorApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;

namespace FlightSimulatorApp.Views
{
    /// <summary>
    /// Interaction logic for UploadWindow.xaml
    /// </summary>
    public partial class UploadWindow : Window
    {
        string xmlFilePath;
        string csvFilePath;
        string fgFolderPath = "C:\\Program Files\\FlightGear 2020.3.6";
        bool wasXmlSelected = false;
        bool wasCsvSelected = false;
        bool inShutdown = true;

        public UploadWindow()
        {
            InitializeComponent();
        }

        /////////////////////////////////////////////////////////////////////////
        private void CSVFile_Button_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".csv";
            dlg.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                csvFilePath = dlg.FileName;
                CSVtextBox.Text = "Selected CSV File:   " + csvFilePath;
                wasCsvSelected = true;
            }

            if (!Directory.Exists("C:\\Program Files\\FlightGear 2020.3.6")) // default path to FlightGear folder doesn't exists
            {
                // show a button to add a path (also a match note for the user)
                btnSetFGPath.Visibility = System.Windows.Visibility.Visible;
                FGtextBox.Text = "Sorry! We can't find the right Path to FlightGear folder. Please add it here.";
            }
            else
            {
                ContinueButton.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void XMLFile_Button_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".csv";
            dlg.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                xmlFilePath = dlg.FileName;
                XMLtextBox.Text = "Selected XML File:   " + xmlFilePath;
                wasXmlSelected = true;

                // extract the XML name from the given path
                MainWindow.XmlName = System.IO.Path.GetFileNameWithoutExtension(dlg.SafeFileName); 
            }
        }

        private void btnSetFGPath_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dlg = new CommonOpenFileDialog();
            dlg.IsFolderPicker = true;
            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                // Open document 
                fgFolderPath = dlg.FileName;
                FGtextBox.Text = "Selected FG Path:   " + fgFolderPath;
                ContinueButton.Visibility = System.Windows.Visibility.Visible;

                // set a path to FlightGear folder.
                MainWindow.FgPath = fgFolderPath;
            }
        }

        private void Continue_Button_Click(object sender, RoutedEventArgs e)
        {
            if (wasCsvSelected)
            {
                MainWindow.getModel().setParserPath(csvFilePath, xmlFilePath); // xmlFilePath could be null
                inShutdown = false;
                this.Close();
                Window main = new MainWindow();
                main.ShowDialog();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (inShutdown)
                Application.Current.Shutdown();
        }

    }
}
