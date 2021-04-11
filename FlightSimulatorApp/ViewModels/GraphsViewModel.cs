using FlightSimulatorApp.Models;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.ViewModels
{
    class GraphsViewModel : Notify
    {

        private ModelGraphs model;

        public GraphsViewModel(ModelGraphs m)
        {
            model = m;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }


        public List<correlatedFeatures> VM_corrFeaturesList
        {
            get { return this.model.CorrFeaturesList; }
        }


        public int VM_timestep
        {
            get { return model.Timestep; }
        }

        public List<string> VM_CsvRows
        {
            get { return this.model.CsvRows; }

        }

        public List<string> VM_XmlColNames
        {
            get { return this.model.XmlColNames; }
        }

        public Dictionary<string, List<float>> VM_DicAsCols
        {
            get { return this.model.DicAsCols; }

        }

        public Dictionary<int, List<float>> VM_DicAsRows
        {
            get { return this.model.DicAsRows; }
        }

        public List<DataPoint> VM_Last300PointsOfSelectedFeature
        {
            get { return model.Last300PointsOfSelectedFeature; }
        }
        public List<DataPoint> VM_Last300PointsOfSelectedFeatureCorrelated
        {
            get { return model.Last300PointsOfSelectedFeatureCorrelated; }
        }

        public string VM_SelectedGraphFeature
        {
            get { return model.SelectedGraphFeature; }
        }

        public string VM_SelectedGraphFeatureCorrelated
        {
            get { return model.SelectedGraphFeatureCorrelated; }
        }


        public string VM_SelectedAnomalyFeature
        {
            get { return model.SelectedAnomalyFeature; }
        }

        public PlotModel VM_AnomalyPlotModel
        {
            get { return model.AnomalyPlotModel; }
        }




        // COMMANDS

        public void parseXML(string xmlPath)
        {
            model.parseXML(xmlPath);
        }

        public void parseCSV(string xmlPath)
        {
            model.parseCSV(xmlPath);
        }

        public void LearnNormal()
        {
            model.LearnNormal();
        }

        public void setAnomaliesWindowOpened(bool value)
        {
            model.setAnomaliesWindowOpened(value);
        }

        public void setDllPath(string dllPath)
        {
            model.setDllPath(dllPath);
        }

        public string GetMostCorrelatedFeatureOf(string feature)
        {
            return model.GetMostCorrelatedFeatureOf(feature);
        }

        public List<float> GetColumnDataByName(string feature)
        {
            return model.GetColumnDataByName(feature);
        }

        public void updateSelectedFeature(string feature)
        {
            model.updateSelectedFeature(feature);
        }

        public void setGraphWindowOpened(bool value)
        {
            model.setGraphWindowOpened(value);
        }

        public void LoadDLL()
        {
            model.LoadDLL();
        }

        public void updateSelectedAnomalyFeature(string feature)
        {
            model.updateSelectedAnomalyFeature(feature);
        }
    }
}
