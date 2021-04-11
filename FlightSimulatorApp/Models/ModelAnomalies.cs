using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Models
{
    class ModelAnomalies : Notify
    {
        private PlotModel anomalyPlotModel;
        public PlotModel AnomalyPlotModel
        {
            get { return this.anomalyPlotModel; }
            set
            {
                if (this.anomalyPlotModel != value)
                {
                    this.anomalyPlotModel = value;
                    this.NotifyPropertyChanged("AnomalyPlotModel");
                }
            }
        }

        private string selectedAnomalyFeature;

        private Assembly assemblyDLL;

        private Type anomalyDetectorType;

        private MethodInfo learnNormalMethod;

        private MethodInfo detectMethod;

        private MethodInfo drawGraphMethod;

        private object Detector;

        private string dllPath;

        private List<DataPoint> anomalousPointsList;
        public List<DataPoint> AnomalousPointsList
        {
            get { return this.anomalousPointsList; }
            set
            {
                if (this.anomalousPointsList != value)
                {
                    this.anomalousPointsList = value;
                    this.NotifyPropertyChanged("AnomalousPointsList");
                }
            }
        }




        public ModelAnomalies(string dllPath)
        {
            this.AnomalyPlotModel = new PlotModel();
            this.dllPath = dllPath;
        }


        public void LoadDLL()
        {

            // Use the file name to load the assembly into the current
            // application domain.
            this.assemblyDLL = Assembly.LoadFile(this.dllPath);
            //this.assemblyDLL.GetName();
            //string filename = dllPath.Split('/').Last();
            //string ClassName = filename.Split('.').First();

            string ClassName = System.IO.Path.GetFileNameWithoutExtension(dllPath);

            // Get the type to use.
            this.anomalyDetectorType = assemblyDLL.GetType(ClassName + ".Algorithm");
            // Create an instance.
            string[] args = { "C:/Users/Or/Documents/Uni/SemesterD/AdvProg2/playback_small.xml" };
            this.Detector = Activator.CreateInstance(this.anomalyDetectorType, args);


            // Get the LearnNormal method.
            this.learnNormalMethod = anomalyDetectorType.GetMethod("LearnNormal");
            // Execute the method.
            object[] lobject = new object[] { "C:/Users/Or/Documents/Uni/SemesterD/AdvProg2/reg_flight.csv", 0 };
            learnNormalMethod.Invoke(Detector, lobject);


            // Get the Detect method.
            this.detectMethod = anomalyDetectorType.GetMethod("Detect");
            // Execute the detect method.
            lobject = new object[] { "C:/Users/Or/Documents/Uni/SemesterD/AdvProg2/anomaly_flight.csv" };
            this.detectMethod.Invoke(Detector, lobject);


            // Get the drawGraph method.
            this.drawGraphMethod = anomalyDetectorType.GetMethod("drawGraph");


        }

        public void updateSelectedAnomalyFeature(string feature)
        {
            this.selectedAnomalyFeature = feature;
            this.updateGraph();
        }

        private void updateGraph()
        {
            object[] lobject = new object[] { selectedAnomalyFeature };
            PlotModel PM = (PlotModel)drawGraphMethod.Invoke(Detector, lobject);
            this.AnomalyPlotModel = PM;
        }
    }
}
