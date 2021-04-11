using OxyPlot;
using System;
using System.Collections.Generic;
using System.IO;
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

        private string csvPath;

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




        public ModelAnomalies(string dllPath, string csvPath)
        {
            this.AnomalyPlotModel = new PlotModel();
            this.dllPath = dllPath;
            this.csvPath = csvPath;
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
            string xml = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources\\playback_small.xml");
            string[] args = { xml };
            this.Detector = Activator.CreateInstance(this.anomalyDetectorType, args);


            // Get the LearnNormal method.
            this.learnNormalMethod = anomalyDetectorType.GetMethod("LearnNormal");
            // Execute the method.
            string csvNormal = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources\\reg_flight.csv");
            object[] lobject = new object[] { csvNormal, 0 };
            learnNormalMethod.Invoke(Detector, lobject);


            // Get the Detect method.
            this.detectMethod = anomalyDetectorType.GetMethod("Detect");
            // Execute the detect method.
            lobject = new object[] { csvPath };
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
