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
                wasCsvSelected = true;
                btnImportNext.Visibility = System.Windows.Visibility.Visible;
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
                MessageBox.Show("Thank you! 'FlightGear' path is updated.");
                btnSetupNext.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void Continue_Button_Click(object sender, RoutedEventArgs e)
        {
            if (wasCsvSelected)
            {
                MainWindow.getModel().setParserPath(csvFilePath, xmlFilePath); // xmlFilePath could be null
                inShutdown = false;
                // set a path to FlightGear folder.
                MainWindow.FgPath = fgFolderPath;

                this.Close();
                Window main = new MainWindow();
                main.ShowDialog();
            }
            else
            {
                MessageBox.Show("Sorry! Can't start the app without a CSV file.");
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (inShutdown)
                Application.Current.Shutdown();
        }

        private void btnWelcome_Click(object sender, RoutedEventArgs e)
        {
            tabContorl.SelectedIndex = 1;

            if (!Directory.Exists("C:\\Program Files\\FlightGear 2020.3.6")) // default path to FlightGear folder doesn't exists
            {
                btnSetupNext.Visibility = System.Windows.Visibility.Hidden;
                fgPathCheck.Visibility = System.Windows.Visibility.Visible;
                // show a button to add a path (also a match note for the user)
                btnSetFGPath.Visibility = System.Windows.Visibility.Visible;
                //FGtextBox.Text = "Sorry! We can't find the right Path to FlightGear folder. Please add it here.";
            }
        }

        private void btnSetupBack_Click(object sender, RoutedEventArgs e)
        {
            tabContorl.SelectedIndex = 0;
        }

        private void btnSetupNext_Click(object sender, RoutedEventArgs e)
        {
            tabContorl.SelectedIndex = 2;
        }

        private void btnImportNext_Click(object sender, RoutedEventArgs e)
        {
            tabContorl.SelectedIndex = 3;
        }

        private void btnImportBack_Click(object sender, RoutedEventArgs e)
        {
            tabContorl.SelectedIndex = 1;
        }

        private void btnDoneBack_Click(object sender, RoutedEventArgs e)
        {
            tabContorl.SelectedIndex = 2;
        }
    }
}
