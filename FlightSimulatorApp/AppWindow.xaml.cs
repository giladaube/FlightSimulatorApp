using FlightSimulatorApp.Views;
using System.Windows;

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for AppWindow.xaml
    /// </summary>
    public partial class AppWindow : Window
    {
        public AppWindow()
        {
            InitializeComponent();
            Window upload = new UploadWindow();
            upload.ShowDialog();
        }
    }
}
