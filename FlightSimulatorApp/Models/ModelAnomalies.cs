using OxyPlot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

/***
 * AnomaliesModel- The model recives a DLL of anomalies detactor algorithm and displayer using
 * it to mark save the anomalies points in the data.
 * ***/

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

        private MethodInfo getAnomaliesLinestepsMethod;

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

        private List<Tuple<int, string>> anomaliesLinesteps;
        public List<Tuple<int, string>> AnomaliesLinesteps
        {
            get { return this.anomaliesLinesteps; }
            set
            {
                if (this.anomaliesLinesteps != value)
                {
                    this.anomaliesLinesteps = value;
                    this.NotifyPropertyChanged("AnomaliesLinesteps");
                }
            }
        }

        private List<string> anomaliesTimesteps;
        public List<string> AnomaliesTimesteps
        {
            get { return this.anomaliesTimesteps; }
            set
            {
                if (this.anomaliesTimesteps != value)
                {
                    this.anomaliesTimesteps = value;
                    this.NotifyPropertyChanged("AnomaliesTimesteps");
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
            // Use the file name to load the assembly into the current application domain.
            this.assemblyDLL = Assembly.LoadFile(this.dllPath);

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
            this.getAnomaliesLinestepsMethod = anomalyDetectorType.GetMethod("GetAnomaliesLinesteps");

            // Get the drawGraph method.
            this.drawGraphMethod = anomalyDetectorType.GetMethod("DrawGraph");
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
            this.AnomalyPlotModel = null;
            GC.Collect();
            this.AnomalyPlotModel = PM;
            List<int> list = (List<int>)getAnomaliesLinestepsMethod.Invoke(Detector, lobject);
            //AnomaliesLinesteps
            
            List<Tuple<int,string>> timesteps = new List<Tuple<int, string>>();
            List<string> timestepsStrings = new List<string>();
            foreach (int i in list)
            {
                int asSeconds = i / 10;
                int hh = (int)asSeconds / 3600;
                int mm = (int)asSeconds / 60;
                int ss = (int)asSeconds - (hh * 3600) - (mm * 60);
                string timestepstr = String.Format("{0:00}:{1:00}:{2:00}", hh, mm, ss);
                bool isInList = false;
                for (int j = 0; j < timesteps.Count; j++)
                {
                    if (timesteps[j].Item1 == asSeconds)
                    {
                        isInList = true;
                    }
                }
                if (!isInList)
                {
                    timesteps.Add(new Tuple<int, string>(asSeconds, timestepstr));
                    timestepsStrings.Add(timestepstr);
                }
            }
            AnomaliesLinesteps = timesteps;
            AnomaliesTimesteps = timestepsStrings;

        }
    }
}
