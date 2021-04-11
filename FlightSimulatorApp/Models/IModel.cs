using OxyPlot;
using System.Collections.Generic;
using System.ComponentModel;

namespace FlightSimulatorApp.Models
{
    public interface IModel : INotifyPropertyChanged
    {
        // PROPERTIES

        double Timestep { set; get; }
        string Timedisplay { get; }
        int TimeSimulator { get; }
        int Linestep { get; set; }

        double Altimeter { set; get; }
        double Airspeed { set; get; }
        double Aileron { set; get; }
        double Elevator { set; get; }
        double Rudder { set; get; }
        double Throttle { set; get; }
        double Heading { set; get; }
        double Roll { set; get; }
        double Pitch { set; get; }
        double Yaw { set; get; }
        // Graphs Properties
        string SelectedGraphFeature { get; }
        string SelectedGraphFeatureCorrelated { get; }
        List<DataPoint> Last300PointsOfSelectedFeature { get; }
        List<DataPoint> Last300PointsOfSelectedFeatureCorrelated { get; }
        List<DataPoint> Last300PointsOfSelectedFeatureAsCorrelated { get; }

        List<string> ColNames { get; }
        

        // Anomalies Properties
        PlotModel AnomalyPlotModel { get; }
        List<DataPoint> AnomalousPointsList { get; }


        // Model COMMANDS
        void end(); // End Simulator
        void setParserPath(string csvPath, string xmlPath);

        // ModelPlayer commands
        void changeRate(double rate);
        void changeTimestep(double timestep);
        void playSimulator();
        void pauseSimulator();

        void play();


        // ModelControls commands


        // ModelGraphs commands
        void updateSelectedFeature(string feature);
        void setDllPath(string dllPath);
        void LearnNormal();
        void setGraphWindowOpened(bool value);

        // ModelAnomalies commands
        void loadDLL();
        void updateSelectedAnomalyFeature(string feature);
    }
}
