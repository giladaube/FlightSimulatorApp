using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace FlightSimulatorApp.Models
{
    public partial class ModelFacade
    {
        // Models Private Member
        private ModelGraphs graphs;

        public string dllPath
        {
            get { return graphs.DllPath; }
        }

        public string SelectedGraphFeature { get { return graphs.SelectedGraphFeature; } }
        public string SelectedGraphFeatureCorrelated { get { return graphs.SelectedGraphFeatureCorrelated; } }
        public List<DataPoint> Last300PointsOfSelectedFeature { get { return graphs.Last300PointsOfSelectedFeature; } }
        public List<DataPoint> Last300PointsOfSelectedFeatureCorrelated { get { return graphs.Last300PointsOfSelectedFeatureCorrelated; } }
        public List<string> ColNames { get { return parser.colNames; } }





        // Initiate models here
        partial void StartFacade_3()
        {
            
            // add ModelGraphs and set its Notify funcions.
            graphs = new ModelGraphs(slinestep, parser );
            player.PropertyChanged +=
                    delegate (Object sender, PropertyChangedEventArgs e)
                    {
                        NotifyPropertyChanged(e.PropertyName);
                    };
        }

        // Ends Models, Processes and Threads.
        partial void EndFacade_3()
        {
            // End Graphs Model.
            graphs.end();
        }

        public void updateSelectedFeature(string feature)
        {
            this.graphs.updateFeaturePoints(feature);
        }

        public void setDllPath(string dllPath)
        {
            this.graphs.setDllPath(dllPath);
        }

        public void LearnNormal()
        {
            this.graphs.LearnNormal();
        }

        public void setGraphWindowOpened(bool value)
        {
            this.graphs.setGraphWindowOpened(value);
        }
    }
}
