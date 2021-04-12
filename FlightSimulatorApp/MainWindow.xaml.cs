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

        private UserControl u1 = new AltimeterView();
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


        private void Compass_Click(object sender, EventArgs e)
        {
            if (comPanel.Visibility == System.Windows.Visibility.Visible)
            {
                this.comPanel.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                this.comPanel.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void Altimeter_Click(object sender, EventArgs e)
        {
            if (altPanel.Visibility == System.Windows.Visibility.Visible)
            {
                this.altPanel.Visibility = System.Windows.Visibility.Collapsed;
                //this.altPanel.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                //this.altPanel.Visibility = System.Windows.Visibility.Collapsed;
                this.altPanel.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void Joystick_Click(object sender, EventArgs e)
        {
            if (joyPanel.Visibility == System.Windows.Visibility.Visible)
            {
                this.joyPanel.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                this.joyPanel.Visibility = System.Windows.Visibility.Visible;
            }
        }
        
        private void Speedometer_Click(object sender, EventArgs e)
        {
            if (spePanel.Visibility == System.Windows.Visibility.Visible)
            {
                this.spePanel.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                this.spePanel.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void RPY_Click(object sender, EventArgs e)
        {
            if (rpyPanel.Visibility == System.Windows.Visibility.Visible)
            {
                this.rpyPanel.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                this.rpyPanel.Visibility = System.Windows.Visibility.Visible;
            }
        }


        private void Window_Closed(object sender, EventArgs e)
        {
            MessageBox.Show("Thank you for using our app, see you soon!");
            Application.Current.Shutdown();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Thank you for using our app, see you soon!");
            Window_Closed(sender, e);
        }

        private void E_Click(object sender, RoutedEventArgs e)
        {
            Graphs graphs = new Graphs();
            graphs.Show();
        }

        private void RPYView_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
