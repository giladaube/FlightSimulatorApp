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

        public correlatedFeatures(string feature1, string feature2, double corr, double threshold)
        {
            this.feature1 = feature1;
            this.feature2 = feature2;
            this.correlation = corr;
            this.threshold = threshold;
        }
    }

    class ModelGraphs : Notify
    {
        // Private fields
        volatile Boolean stop; // volatile indicates that the field might be modified by multiple threads

        private int numOfRows;
        private int numOfCols;
        private SharedLinestep linestep;
        private csvParser parser;

        // Constructor
        public ModelGraphs(SharedLinestep linestep, csvParser parser)
        {
            this.linestep = linestep;
            this.parser = parser;
        }

        // PROPERTIES
        private List<correlatedFeatures> corrFeaturesList;


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


        public void setGraphWindowOpened(bool value)
        {
            this.GraphWindowOpened = value;
        }

        public void setDllPath(string dllPath)
        {
            this.dllPath = dllPath;
        }

        public void updateSelectedFeature(string feature)
        {
            this.selectedGraphFeature = feature;
            this.selectedGraphFeatureCorrelated = GetMostCorrelatedFeatureOf(feature);

            this.Last300PointsOfSelectedFeature = this.updateFeaturePoints(selectedGraphFeature);
            this.Last300PointsOfSelectedFeatureCorrelated = this.updateFeaturePoints(selectedGraphFeatureCorrelated);
        }
       

        public List<DataPoint> updateFeaturePoints(string feature)
        {
            int start = 0;
            if (linestep.Linestep > 300)
            {
                start = linestep.Linestep - 300;
            }

            List<float> FeatureValues = parser.dicAsCols[feature];

            List<DataPoint> Points = new List<DataPoint>();

            for (int i = start; i <= linestep.Linestep; i++)
            {
                DataPoint Point = new DataPoint(i, Convert.ToDouble(FeatureValues[i]));
                Points.Add(Point);
            }

            return Points;
        }


        public void end()
        {
            // Close Windows somehow
            //throw new NotImplementedException();
        }

        public void LearnNormal()
        {
            corrFeaturesList = new List<correlatedFeatures>();
            List<string> titles = parser.colNames;
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
                    columnData[i, j] = parser.dicAsCols[titles.ElementAt(i)].ElementAt(j);
                }
            }

            for (int i = 0; i < titles.Count; i++)
            {
                string f1 = titles[i];
                double maxCorrValue = 0;
                int mostCorrelativeCol = 0;
                for (int j = i + 1; j < titles.Count; j++)
                {
                    double p = Math.Abs(Utilities.pearson(parser.dicAsCols[titles[i]].ToArray(), parser.dicAsCols[titles[j]].ToArray(), len));

                    if (p > maxCorrValue)
                    {
                        maxCorrValue = p;
                        mostCorrelativeCol = j;
                    }
                }
                string f2 = titles[mostCorrelativeCol];


                List<float> x = parser.dicAsCols[f1];
                List<float> y = parser.dicAsCols[f2];

                List<System.Windows.Point> points = new List<System.Windows.Point>();
                for (int k = 0; k < x.Count; k++)
                {
                    points.Add(new System.Windows.Point(x[k], y[k]));
                }

                Line line = Utilities.linear_reg(points, numOfRows);
                double currentThreshold = Utilities.calcThreshold(points, line, numOfRows);
                correlatedFeatures c = new correlatedFeatures(f1, f2, maxCorrValue, currentThreshold);

                corrFeaturesList.Add(c);
            }

        }

        private string GetMostCorrelatedFeatureOf(string feature)
        {
            foreach (correlatedFeatures cf in corrFeaturesList)
            {
                if (cf.feature1.Equals(feature))
                {
                    return cf.feature2;
                }
            }
            return "noFeature";
        }

        private List<float> GetColumnDataByName(string feature)
        {
            return parser.dicAsCols[feature];
        }





    }
}
