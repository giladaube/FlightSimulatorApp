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

namespace FlightSimulatorApp.Views
{
    /// <summary>
    /// Interaction logic for PlayerView.xaml
    /// </summary>
    public partial class PlayerView : UserControl
    {
        private Process fgProcess; // FG process id.
        private ViewModelPlayer vm;
        public PlayerView()
        {
            InitializeComponent();
            this.vm = new ViewModelPlayer(MainWindow.getModel());
            this.DataContext = this.vm;

            App.Current.Exit += delegate (object sender, ExitEventArgs e)
            {
                this.close();
            };
        }

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            vm.play();
        }

        private void pauseButton_Click(object sender, RoutedEventArgs e)
        {
            vm.pause();
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
            vm.close();
            if (this.fgProcess != null && !fgProcess.HasExited) // FG process still running.
            {
                this.fgProcess.CloseMainWindow(); // stops FG process.
                this.fgProcess.Close(); // clear all used resources.
            }
        }

        private void fgButton_Click(object sender, RoutedEventArgs e)
        {
            this.fgProcess = new Process(); // create a new Process.
            this.fgProcess.StartInfo.RedirectStandardOutput = true;
            this.fgProcess.StartInfo.UseShellExecute = false;
            this.fgProcess.StartInfo.CreateNoWindow = true;

            this.fgProcess.StartInfo.WorkingDirectory = "C:\\Program Files\\FlightGear 2020.3.6\\data"; // set working directory for FG.
            this.fgProcess.StartInfo.FileName = "c:\\program files\\flightgear 2020.3.6\\bin\\fgfs.exe"; // set process to open FG.
            this.fgProcess.StartInfo.Arguments = "--httpd=8080 --generic=socket,in,10,127.0.0.1,5400,tcp,playback_small --fdm=null"; // set FG to listen on port 5400.
            ThreadStart fgThs = new ThreadStart(() => this.fgProcess.Start()); // set thread to start process.
            Thread fgThread = new Thread(fgThs);
            fgThread.Start(); // execute thread and start FG as another process.  
            //dockFG();
        }

        private void dockFG()
        {
            Thread.Sleep(500);
            IntPtr hWndInsertAfter = new IntPtr(-1); //HWND_TOPMOST (HWND) - 1
            NativeMethods.SetWindowPos(fgProcess.MainWindowHandle, hWndInsertAfter, 0, 0, Convert.ToInt32(Window.GetWindow(this).Width), Convert.ToInt32(Window.GetWindow(this).Height), 0);
            IntPtr windowHandle = new WindowInteropHelper(Window.GetWindow(this)).Handle;
            NativeMethods.SetParent(fgProcess.MainWindowHandle, windowHandle);

            NativeMethods.SetWindowLongPtr(fgProcess.MainWindowHandle, (GWL)(int)GWL.STYLE,
                NativeMethods.GetWindowLongPtr(fgProcess.MainWindowHandle, (GWL)(int)GWL.STYLE));

            NativeMethods.SetWindowPos(fgProcess.MainWindowHandle, IntPtr.Zero, 0, 0, Convert.ToInt32(Window.GetWindow(this).Height) * 3, Convert.ToInt32(Window.GetWindow(this).Height) * 2, 0);

        }
    }
}
