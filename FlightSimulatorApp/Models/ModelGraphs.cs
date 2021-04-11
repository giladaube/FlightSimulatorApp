using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace FlightSimulatorApp.Models
{
    public struct correlatedFeatures
    {
        public string feature1, feature2;
        public double correlation;
        //public Line lin_reg;
        public double threshold;
        //public double PointX;
        //public double PointY;

        public correlatedFeatures(string feature1, string feature2, double corr, double threshold)
        {
            this.feature1 = feature1;
            this.feature2 = feature2;
            this.correlation = corr;
            this.threshold = threshold;
            //this.PointX = PointX;
            //this.PointY = PointY;
        }
    }

    class ModelGraphs : Notify
    {
        // Private fields
        private IClient client;
        volatile Boolean stop; // volatile indicates that the field might be modified by multiple threads

        private int numOfRows;
        private int numOfCols;


        // Constructor
        public ModelGraphs(IClient client, int timestep)
        {

            this.Timestep = 0;
            //this.milliseconds = 0;
            this.client = client;
            this.stop = true;
            this.AnomalyPlotModel = new PlotModel();
            start();

        }

        // PROPERTIES
        private List<correlatedFeatures> corrFeaturesList;
        public List<correlatedFeatures> CorrFeaturesList
        {
            get { return this.corrFeaturesList; }
            set
            {
                if (this.corrFeaturesList != value)
                {
                    this.corrFeaturesList = value;
                    this.NotifyPropertyChanged("corrFeaturesList");
                }
            }
        }

        // milliseconds Property
        public int milliseconds
        {
            get { return this.milliseconds; }
            set
            {
                if (this.milliseconds != value)
                {
                    this.milliseconds = value;
                    this.NotifyPropertyChanged("milliseconds");
                }
            }
        }

        private int timestep;
        // timestep Property
        public int Timestep
        {
            get { return this.timestep; }
            set
            {
                if (this.timestep != value)
                {
                    this.timestep = value;
                    this.NotifyPropertyChanged("Timestep");
                }
            }
        }

        private bool graphWindowOpened;
        public bool GraphWindowOpened
        {
            get { return this.graphWindowOpened; }
            set
            {
                if (this.graphWindowOpened != value)
                {
                    this.graphWindowOpened = value;
                    this.NotifyPropertyChanged("GraphWindowOpened");
                }
            }
        }

        private bool anomaliesWindowOpened;
        public bool AnomaliesWindowOpened
        {
            get { return this.anomaliesWindowOpened; }
            set
            {
                if (this.anomaliesWindowOpened != value)
                {
                    this.anomaliesWindowOpened = value;
                    this.NotifyPropertyChanged("AnomaliesWindowOpened");
                }
            }
        }



        //public csvParser csvParser;

        private List<string> csvRows;
        public List<string> CsvRows
        {
            get { return this.csvRows; }
            set
            {
                if (this.csvRows != value)
                {
                    this.csvRows = value;
                    this.NotifyPropertyChanged("CsvRows");
                }
            }
        }

        private List<string> xmlColNames;
        public List<string> XmlColNames
        {
            get { return this.xmlColNames; }
            set
            {
                if (this.xmlColNames != value)
                {
                    this.xmlColNames = value;
                    this.NotifyPropertyChanged("XmlColNames");
                }
            }
        }

        private Dictionary<string, List<float>> dicAsCols;
        public Dictionary<string, List<float>> DicAsCols
        {
            get { return this.dicAsCols; }
            set
            {
                if (this.dicAsCols != value)
                {
                    this.dicAsCols = value;
                    this.NotifyPropertyChanged("DicAsCols");
                }
            }
        }

        private Dictionary<int, List<float>> dicAsRows;
        public Dictionary<int, List<float>> DicAsRows
        {
            get { return this.dicAsRows; }
            set
            {
                if (this.dicAsRows != value)
                {
                    this.dicAsRows = value;
                    this.NotifyPropertyChanged("DicAsRows");
                }
            }
        }

        private List<DataPoint> last300PointsOfSelectedFeature;
        public List<DataPoint> Last300PointsOfSelectedFeature
        {
            get { return this.last300PointsOfSelectedFeature; }
            set
            {
                if (this.last300PointsOfSelectedFeature != value)
                {
                    this.last300PointsOfSelectedFeature = value;
                    this.NotifyPropertyChanged("Last300PointsOfSelectedFeature");
                }
            }
        }

        private List<DataPoint> last300PointsOfSelectedFeatureCorrelated;
        public List<DataPoint> Last300PointsOfSelectedFeatureCorrelated
        {
            get { return this.last300PointsOfSelectedFeatureCorrelated; }
            set
            {
                if (this.last300PointsOfSelectedFeatureCorrelated != value)
                {
                    this.last300PointsOfSelectedFeatureCorrelated = value;
                    this.NotifyPropertyChanged("Last300PointsOfSelectedFeatureCorrelated");
                }
            }
        }

        private string selectedGraphFeature;
        public string SelectedGraphFeature
        {
            get { return this.selectedGraphFeature; }
            set
            {
                if (this.selectedGraphFeature != value)
                {
                    this.selectedGraphFeature = value;
                    this.NotifyPropertyChanged("SelectedGraphFeature");
                }
            }
        }

        private string selectedGraphFeatureCorrelated;
        public string SelectedGraphFeatureCorrelated
        {
            get { return this.selectedGraphFeatureCorrelated; }
            set
            {
                if (this.selectedGraphFeatureCorrelated != value)
                {
                    this.selectedGraphFeatureCorrelated = value;
                    this.NotifyPropertyChanged("SelectedGraphFeatureCorrelated");
                }
            }
        }

        private string lineStyle;
        public string LineStyle
        {
            get { return this.lineStyle; }
            set
            {
                if (this.lineStyle != value)
                {
                    this.lineStyle = value;
                    this.NotifyPropertyChanged("LineStyle");
                }
            }
        }

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

        private string dllPath;
        public string DllPath
        {
            get { return this.dllPath; }
            set
            {
                if (this.dllPath != value)
                {
                    this.dllPath = value;
                    this.NotifyPropertyChanged("DllPath");
                }
            }
        }

        private string selectedAnomalyFeature;
        public string SelectedAnomalyFeature
        {
            get { return this.selectedAnomalyFeature; }
            set
            {
                if (this.selectedAnomalyFeature != value)
                {
                    this.selectedAnomalyFeature = value;
                    this.NotifyPropertyChanged("SelectedAnomalyFeature");
                }
            }
        }

        private Assembly assemblyDLL;
        public Assembly AssemblyDLL
        {
            get { return this.assemblyDLL; }
            set
            {
                if (this.assemblyDLL != value)
                {
                    this.assemblyDLL = value;
                    this.NotifyPropertyChanged("AssemblyDLL");
                }
            }
        }

        private Type anomalyDetectorType;
        public Type AnomalyDetectorType
        {
            get { return this.anomalyDetectorType; }
            set
            {
                if (this.anomalyDetectorType != value)
                {
                    this.anomalyDetectorType = value;
                    this.NotifyPropertyChanged("AnomalyDetectorType");
                }
            }
        }

        private MethodInfo learnNormalMethod;

        private MethodInfo detectMethod;

        private MethodInfo drawGraphMethod;

        private object Detector;





        public void setGraphWindowOpened(bool value)
        {
            this.GraphWindowOpened = value;
        }
        public void setAnomaliesWindowOpened(bool value)
        {
            this.AnomaliesWindowOpened = value;
        }
        public void setDllPath(string dllPath)
        {
            this.DllPath = dllPath;
        }
        private void update300Points()
        {
            this.Last300PointsOfSelectedFeature = this.updateFeaturePoints(selectedGraphFeature);
            this.Last300PointsOfSelectedFeatureCorrelated = this.updateFeaturePoints(selectedGraphFeatureCorrelated);
            //MessageBox.Show("300 points");

        }
        public void updateSelectedFeature(string feature)
        {
            this.selectedGraphFeature = feature;
            this.selectedGraphFeatureCorrelated = GetMostCorrelatedFeatureOf(feature);

            this.Last300PointsOfSelectedFeature = this.updateFeaturePoints(selectedGraphFeature);
            this.Last300PointsOfSelectedFeatureCorrelated = this.updateFeaturePoints(selectedGraphFeatureCorrelated);
            //MessageBox.Show("300 points");
        }
        public void parseXML(string xmlPath)
        {
            if (xmlPath.Equals("default"))
            {
                XmlColNames = new List<string>{"aileron",
    "elevator",
    "rudder",
    "flaps",
    "slats",
    "speedbrake",
    "throttle",
    "throttle1",
    "engine-pump",
    "engine-pump1",
    "electric-pump",
    "electric-pump1",
    "external-power",
    "APU-generator",
    "latitude-deg",
    "longitude-deg",
    "altitude-ft",
    "roll-deg",
    "pitch-deg",
    "heading-deg",
    "side-slip-deg",
    "airspeed-kt",
    "glideslope",
    "vertical-speed-fps",
    "airspeed-indicator_indicated-speed-kt",
    "altimeter_indicated-altitude-ft",
    "altimeter_pressure-alt-ft",
    "attitude-indicator_indicated-pitch-deg",
    "attitude-indicator_indicated-roll-deg",
    "attitude-indicator_internal-pitch-deg",
    "attitude-indicator_internal-roll-deg",
    "encoder_indicated-altitude-ft",
    "encoder_pressure-alt-ft",
    "gps_indicated-altitude-ft",
    "gps_indicated-ground-speed-kt",
    "gps_indicated-vertical-speed",
    "indicated-heading-deg",
    "magnetic-compass_indicated-heading-deg",
    "slip-skid-ball_indicated-slip-skid",
    "turn-indicator_indicated-turn-rate",
    "vertical-speed-indicator_indicated-speed-fpm",
    "engine_rpm" };
            }
            else
            {
                // Implement XML Column Names Ejection
                //System.Windows.Forms.MessageBox.Show("No XML");
            }
        }
        public List<DataPoint> updateFeaturePoints(string feature)
        {
            int start = 0;
            if (Timestep > 300)
            {
                start = Timestep - 300;
            }

            List<float> FeatureValues = dicAsCols[feature];

            List<DataPoint> Points = new List<DataPoint>();

            for (int i = start; i <= Timestep; i++)
            {
                DataPoint Point = new DataPoint(i, Convert.ToDouble(FeatureValues[i]));
                Points.Add(Point);
            }

            return Points;
        }
        public void parseCSV(string csvPath)
        {
            csvParser csvp = new csvParser(csvPath);
            csvp.scanCsv();
            this.numOfCols = csvp.numOfCols;
            this.numOfRows = csvp.numOfRows;
            this.DicAsCols = csvp.dicAsCols;
            //this.DicAsRows = csvp.dicAsRows;
            this.CsvRows = csvp.rowsList;
        }



        // ///// /////////////////////////////////////////////////////





        public void connect(string ip, int port)
        {
            client.connect(ip, port);
        }

        public void disconnect()
        {
            client.disconnect();
            this.stop = true;
        }

        public void start()
        {
            new Thread(delegate ()
            {

                while (stop)
                {
                    //client.write(csvRows.ElementAt(timestep));
                    Timestep++;
                    if (Timestep >= 2100)
                    {
                        Timestep = 1;
                    }

                    if (GraphWindowOpened)
                    {
                        update300Points();
                    }



                    // if we get data back:
                    //string someString = client.read();

                    Thread.Sleep(100); // SOMEHOW KNOW THE KETZEV
                }
            }).Start();
        }

        public void pause()
        {
            this.stop = true;
        }

        public void play()
        {
            start();
            this.stop = false;
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public void changeMilliseconds(int milliseconds)
        {
            this.milliseconds = milliseconds;
        }

        public void changeTimestep(int timestep)
        {
            this.timestep = timestep;
        }

        public void end()
        {
            throw new NotImplementedException();
        }

        public void LearnNormal()
        {
            corrFeaturesList = new List<correlatedFeatures>();
            List<string> titles = XmlColNames;
            //int len = ts.getRowSize();
            int len = this.numOfRows;

            float[,] columnData = new float[titles.Count, len];
            float[][] columns = new float[titles.Count][];
            //float columnData[titles.size()][len];

            for (int i = 0; i < titles.Count; i++)
            {
                //for (int j = 0; j < ts.getRowSize(); j++)
                for (int j = 0; j < len; j++)
                {
                    //columnData[i,j] = ts.getColumnData(titles[i])[j];
                    columnData[i, j] = dicAsCols[titles.ElementAt(i)].ElementAt(j);
                }
            }

            for (int i = 0; i < titles.Count; i++)
            {
                string f1 = titles[i];
                double maxCorrValue = 0;
                int mostCorrelativeCol = 0;
                for (int j = i + 1; j < titles.Count; j++)
                {
                    double p = Math.Abs(Utilities.pearson(DicAsCols[titles[i]].ToArray(), DicAsCols[titles[j]].ToArray(), len));

                    if (p > maxCorrValue)
                    {
                        maxCorrValue = p;
                        mostCorrelativeCol = j;
                    }
                }
                string f2 = titles[mostCorrelativeCol];

                //vector<float> x = ts.getColumnData(f1);
                //vector<float> y = ts.getColumnData(f2);
                List<float> x = dicAsCols[f1];
                List<float> y = dicAsCols[f2];

                //Point** ps = new Point*[x.size()];
                List<System.Windows.Point> points = new List<System.Windows.Point>();
                for (int k = 0; k < x.Count; k++)
                {
                    points.Add(new System.Windows.Point(x[i], y[i]));
                }

                //if (maxCorrValue > 0.9)
                //{

                //correlatedFeatures c = new correlatedFeatures(f1, f2, maxCorrValue, c;


                Line line = Utilities.linear_reg(points, numOfRows);
                double currentThreshold = Utilities.calcThreshold(points, line, numOfRows);

                //c.feature1 = f1;
                //c.feature2 = f2;
                //c.correlation = maxCorrValue;
                ////c.lin_reg = line;
                //c.threshold = currentThreshold;
                correlatedFeatures c = new correlatedFeatures(f1, f2, maxCorrValue, currentThreshold);

                corrFeaturesList.Add(c);
                //}

            }



            Console.WriteLine("end of LearnNormal in Model");

        }

        public string GetMostCorrelatedFeatureOf(string feature)
        {
            foreach (correlatedFeatures cf in CorrFeaturesList)
            {
                if (cf.feature1.Equals(feature))
                {
                    return cf.feature2;
                }
            }
            return "noFeature";
        }

        public List<float> GetColumnDataByName(string feature)
        {
            return DicAsCols[feature];
        }

        private PointCollection getPoints(List<float> feature)
        {
            PointCollection points = new PointCollection();

            int start = 0;
            if (Timestep > 300)
            {
                start = Timestep - 300;
            }

            for (int i = start; i <= Timestep; i++)
            {
                Point p = new Point();
                p.X = i;
                p.Y = feature.ElementAt(i);
                points.Add(p);
            }


            return points;
        }


        public void LoadDLL()
        {

            // Use the file name to load the assembly into the current
            // application domain.
            //this.AssemblyDLL = Assembly.LoadFile(this.dllPath);// "C:/Users/Or/source/repos/linearRegression/bin/Debug/linearRegression.dll");

            this.AssemblyDLL = Assembly.LoadFile("C:/Users/Or/source/repos/linearRegression/bin/Debug/linearRegression.dll");

            // Get the type to use.
            this.AnomalyDetectorType = AssemblyDLL.GetType("linearRegression.LinearRegression");
            // Create an instance.
            string[] args = { "C:/Users/Or/Documents/Uni/SemesterD/AdvProg2/playback_small.xml" };
            this.Detector = Activator.CreateInstance(this.AnomalyDetectorType, args);


            // Get the method to call.
            this.learnNormalMethod = AnomalyDetectorType.GetMethod("LearnNormal");
            // Execute the method.
            object[] lobject = new object[] { "C:/Users/Or/Documents/Uni/SemesterD/AdvProg2/reg_flight.csv", 0 };
            learnNormalMethod.Invoke(Detector, lobject);


            // Get the method to call.
            this.detectMethod = AnomalyDetectorType.GetMethod("GetAnomalousDataPoints");
            // Execute the method.
            lobject = new object[] { "C:/Users/Or/Documents/Uni/SemesterD/AdvProg2/anomaly_flight.csv" };
            this.detectMethod.Invoke(Detector, lobject);


            // Get the method to call.
            this.drawGraphMethod = AnomalyDetectorType.GetMethod("drawGraph");
            // Execute the method.
            //lobject = new object[] { "aileron" };
            //PlotModel PM = (PlotModel)drawGraphMethod.Invoke(Detector, lobject);

            //this.AnomalyPlotModel = PM;

        }

        public void updateGraph()
        {
            object[] lobject = new object[] { SelectedAnomalyFeature };
            PlotModel PM = (PlotModel)drawGraphMethod.Invoke(Detector, lobject);
            this.AnomalyPlotModel = PM;
        }


        private void setUpPlotModel()
        {
            PlotModel PM = new PlotModel();
            PM.LegendTitle = "Legend";
            PM.LegendOrientation = LegendOrientation.Horizontal;
            PM.LegendPlacement = LegendPlacement.Outside;
            PM.LegendPosition = LegendPosition.TopRight;
            PM.LegendBackground = OxyColor.FromAColor(200, OxyColors.White);
            PM.LegendBorder = OxyColors.Black;

            // AXES
            PM.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Minimum = -20, Maximum = 20 });
            PM.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Minimum = -20, Maximum = 20 });

            // TWO HALF CIRCLES
            PM.Series.Add(new FunctionSeries((x) => Math.Sqrt(Math.Max(16 - Math.Pow(x, 2), 0)), -4, 4, 0.1, "x^2 + y^2 = 16") { Color = OxyColors.Red });
            PM.Series.Add(new FunctionSeries((x) => -Math.Sqrt(Math.Max(16 - Math.Pow(x, 2), 0)), -4, 4, 0.1) { Color = OxyColors.Red });

            AnomalyPlotModel = PM;
        }

        public void updateSelectedAnomalyFeature(string feature)
        {
            //setUpPlotModel();
            //PlotModel PM = new PlotModel();
            this.SelectedAnomalyFeature = feature;
            this.updateGraph();

            //FunctionSeries fs = new FunctionSeries();
            ////fs.Points.Add(new DataPoint(0, 0));
            ////fs.Points.Add(new DataPoint(10, 10));
            //PM.Series.Add(new FunctionSeries((x) => Math.Sqrt(Math.Max(16 - Math.Pow(x, 2), 0)), -4, 4, 0.1, "x^2 + y^2 = 16") { Color = OxyColors.Red });

            //PM.Series.Add(fs);

            //AnomalyPlotModel = PM;
        }

    }
}
