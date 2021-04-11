using FlightSimulatorApp.Models;
using FlightSimulatorApp.ViewModels;
using FlightSimulatorApp.Views;
using System;
using System.Collections.Generic;
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

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // when user enter a path to csv need to use:: setCSVPath(string path); in IModel
        private static IModel model = new ModelFacade();

        private static string fgPath;
        public static string FgPath
        {
            get { return fgPath; }
            set
            {
                fgPath = value;
            }
        }

        private static string xmlName = "playback_small";
        public static string XmlName
        {
            get { return xmlName; }
            set
            {
                xmlName = value;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        // static function to get the global model.
        public static IModel getModel()
        {
            return model;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Thank you for using our app, see you soon!");
            Window_Closed(sender, e);
        }
    }
}
