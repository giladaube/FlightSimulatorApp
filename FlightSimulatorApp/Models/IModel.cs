using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Models
{
    public interface IModel : INotifyPropertyChanged
    {
        // PROPERTIES

        double Timestep { set; get; }
        string Timedisplay { get; }

        // Model COMMANDS
        void end(); // End Simulator

        // ModelPlayer commands
        void changeRate(double rate);
        void changeTimestep(double timestep);
        void playSimulator();
        void pauseSimulator();

    }
}
