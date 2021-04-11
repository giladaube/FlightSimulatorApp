using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace FlightSimulatorApp.Models
{
    public struct correlatedFeatures
    {
        public string feature1, feature2;
        public double correlation;
        public Line lin_reg;
        public double threshold;

        public correlatedFeatures(string feature1, string feature2, Line line, double corr, double threshold)
        {
            this.feature1 = feature1;
            this.feature2 = feature2;
            this.lin_reg = line;
            this.correlation = corr;
            this.threshold = threshold;
        }
    }

    class ModelGraphs : Notify
    {
        private SharedLinestep linestep;
        private csvParser parser;
        private Thread updateGraphsThread;

        // Constructor
        public ModelGraphs(SharedLinestep linestep, csvParser parser)
        {
            this.linestep = linestep;
            this.parser = parser;
        }

        // PROPERTIES
        private List<correlatedFeatures> corrFeaturesList;


        private bool graphWindowOpened;

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
        private List<DataPoint> linearRegression;


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

        private List<DataPoint> last300PointsOfSelectedFeatureAsCorrelated;
        public List<DataPoint> Last300PointsOfSelectedFeatureAsCorrelated
        {
            get { return this.last300PointsOfSelectedFeatureAsCorrelated; }
            set
            {
                if (this.last300PointsOfSelectedFeatureAsCorrelated != value)
                {
                    this.last300PointsOfSelectedFeatureAsCorrelated = value;
                    this.NotifyPropertyChanged("Last300PointsOfSelectedFeatureAsCorrelated");
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
            this.graphWindowOpened = value;
            if (value)
            {
                updateGraphs();
            }
            else
            {
                if (updateGraphsThread.IsAlive)
                {
                    updateGraphsThread.Abort();
                }
            }
        }

        public void setDllPath(string dllPath)
        {
            this.DllPath = dllPath;

        }

        private void updateGraphs()
        {
            int line = this.linestep.Linestep;
            ThreadStart thread = new ThreadStart(() =>
            {
                Thread.Sleep(500);
                while (graphWindowOpened)
                {
                    while (line != this.linestep.Linestep)
                    {
                        if (SelectedGraphFeature != null)
                        {
                            this.Last300PointsOfSelectedFeature = this.updateFeaturePoints(SelectedGraphFeature);
                            this.Last300PointsOfSelectedFeatureCorrelated = this.updateFeaturePoints(SelectedGraphFeatureCorrelated);
                            this.Last300PointsOfSelectedFeatureAsCorrelated = this.updateFeatureAsCorrelatedPoints();
                            line = this.linestep.Linestep;
                        }
                    }
                }
            });
            updateGraphsThread = new Thread(thread);
            updateGraphsThread.Start();
        }

        public void updateSelectedFeature(string feature)
        {
            if (feature != null)
            {
                this.SelectedGraphFeature = feature;
                this.SelectedGraphFeatureCorrelated = GetMostCorrelatedFeatureOf(feature);
                this.updateLinearRegression();

                this.Last300PointsOfSelectedFeature = this.updateFeaturePoints(SelectedGraphFeature);
                this.Last300PointsOfSelectedFeatureCorrelated = this.updateFeaturePoints(SelectedGraphFeatureCorrelated);
                this.Last300PointsOfSelectedFeatureAsCorrelated = this.updateFeatureAsCorrelatedPoints();

            }
        }


        private void updateLinearRegression()
        {
            List<float> FeatureValues = parser.dicAsCols[SelectedGraphFeature];

            Line line = new Line(0, 0);
            foreach (correlatedFeatures cf in corrFeaturesList)
            {
                if (cf.feature1.Equals(SelectedGraphFeature))
                {
                    line = cf.lin_reg;
                }
            }

            List<DataPoint> Points = new List<DataPoint>();

            double xmin = 99999, xmax = -99999;
            double ymin = 0, ymax = 0;
            for (int i = 0; i < parser.numOfRows; i++)
            {
                if (FeatureValues[i] < xmin)
                {
                    xmin = FeatureValues[i];
                    ymin = line.f(xmin);
                }
                if (FeatureValues[i] > xmax)
                {
                    xmax = FeatureValues[i];
                    ymax = line.f(xmax);
                }
            }

            Points.Add(new DataPoint(xmin, ymin));
            Points.Add(new DataPoint(xmax, ymax));

            this.linearRegression = Points;
        }


        private List<DataPoint> updateFeatureAsCorrelatedPoints()
        {
            int numOfPoints = Last300PointsOfSelectedFeature.Count;
            List<DataPoint> points = new List<DataPoint>();
            for (int i = 0; i < numOfPoints - 2; i++)
            {
                DataPoint point = new DataPoint(Last300PointsOfSelectedFeature[i].Y, Last300PointsOfSelectedFeatureCorrelated[i].Y);
                points.Add(point);
            }
            return points;
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
            setGraphWindowOpened(false);
        }

        public void LearnNormal()
        {
            corrFeaturesList = new List<correlatedFeatures>();
            List<string> titles = parser.colNames;
            int len = parser.numOfRows;

            float[,] columnData = new float[titles.Count, len];
            float[][] columns = new float[titles.Count][];

            for (int i = 0; i < titles.Count; i++)
            {
                for (int j = 0; j < len; j++)
                {
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

                Line line = Utilities.linear_reg(points, parser.numOfRows);
                double currentThreshold = Utilities.calcThreshold(points, line, parser.numOfRows);

                corrFeaturesList.Add(new correlatedFeatures(f1, f2, line, maxCorrValue, currentThreshold));
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
    }
}
