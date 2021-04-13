using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Models
{
    partial class ModelFacade
    {
        // Models Private Member
        private ModelAnomalies anomalies;

        // Initiate models here
        partial void StartFacade_4()
        {
            // add ModelGraphs and set its Notify funcions.
            anomalies = new ModelAnomalies(dllPath, csvPath);
            anomalies.PropertyChanged +=
                    delegate (Object sender, PropertyChangedEventArgs e)
                    {
                        NotifyPropertyChanged(e.PropertyName);
                    };
        }

        // Ends Models, Processes and Threads.
        partial void EndFacade_4()
        {
            // End Anomalies Model.
            //anomalies.end();
        }


        public void loadDLL()
        {
            anomalies.LoadDLL();
        }
        public void updateSelectedAnomalyFeature(string feature)
        {
            anomalies.updateSelectedAnomalyFeature(feature);
        }


        public PlotModel AnomalyPlotModel { get { return anomalies.AnomalyPlotModel; } }
        public List<DataPoint> AnomalousPointsList { get { return anomalies.AnomalousPointsList; } }

        public List<int> AnomaliesLinesteps
        {
            get { return anomalies.AnomaliesLinesteps; }
        }
    }
}
