using System.ComponentModel;

namespace FlightSimulatorApp.Models
{
    public interface IModel : INotifyPropertyChanged
    {
        // PROPERTIES

        double Timestep { set; get; }
        string Timedisplay { get; }
        int TimeSimulator { get; }

        // Model COMMANDS
        void end(); // End Simulator
        void setParserPath(string csvPath, string xmlPath);

        // ModelPlayer commands
        void changeRate(double rate);
        void changeTimestep(double timestep);
        void playSimulator();
        void pauseSimulator();

    }
}
