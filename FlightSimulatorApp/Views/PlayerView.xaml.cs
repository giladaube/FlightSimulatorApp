using FlightSimulatorApp.Models;
using FlightSimulatorApp.ViewModels;
using FlightSimulatorApp;
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
using System.Diagnostics;
using System.Threading;
using ControlzEx.Standard;
using System.Windows.Interop;
using System.Runtime.InteropServices;

namespace FlightSimulatorApp.Views
{
    /// <summary>
    /// Interaction logic for PlayerView.xaml
    /// </summary>
    public partial class PlayerView : UserControl
    {
        private FG fg; // FG object.
        private Boolean soundOn = true;
        private ViewModelPlayer vm;
        public PlayerView()
        {
            InitializeComponent();
            this.vm = new ViewModelPlayer(MainWindow.getModel());
            this.DataContext = this.vm;
            this.fg = new FG();

            App.Current.Exit += delegate (object sender, ExitEventArgs e)
            {
                this.close();
            };
        }

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            vm.play();
            if (!soundOn)
            {
                fg.soundHandle();
                soundOn = true;
            }
        }

        private void pauseButton_Click(object sender, RoutedEventArgs e)
        {
            vm.pause();
            if (soundOn)
            {
                fg.soundHandle();
                soundOn = false;
            }
        }

        private void Rate_dropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (vm != null && Rate_dropdown.SelectedItem != null) // make sure vm is up and running.
            {
                double rate;
                switch (Rate_dropdown.SelectedIndex) // locate selected item and change Rate accordingly.
                {
                    case 0:
                        rate = 0.5;
                        break;
                    case 2:
                        rate = 1.5;
                        break;
                    case 3:
                        rate = 2.0;
                        break;
                    default:
                        rate = 1.0;
                        break;
                }
                vm.changeRate(rate);
            }
        }

        private void Slider_Timestep_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            vm.changeTimestep(Slider_Timestep.Value);
        }

        public void close()
        {
            if (!soundOn)
            {
                fg.soundHandle();
                soundOn = true;
            }
            vm.close();
            fg.close();
        }

        private void fgButton_Click(object sender, RoutedEventArgs e)
        {
            fg.start();
            //fg.dockFG(Window.GetWindow(this));
            fgButton.IsEnabled = false;
        }
    }
}
