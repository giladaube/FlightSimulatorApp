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
    class ViewModelGraphs : Notify
    {

        private IModel model;

        public ViewModelGraphs(IModel model)
        {
            this.model = model;
            model.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_" + e.PropertyName);
                };
        }



        public List<string> VM_ColNames
        {
            get { return this.model.ColNames; }
        }


        public List<DataPoint> VM_Last300PointsOfSelectedFeature
        {
            get { return model.Last300PointsOfSelectedFeature; }
        }
        public List<DataPoint> VM_Last300PointsOfSelectedFeatureCorrelated
        {
            get { return model.Last300PointsOfSelectedFeatureCorrelated; }
        }
        public List<DataPoint> VM_Last300PointsOfSelectedFeatureAsCorrelated
        {
            get { return model.Last300PointsOfSelectedFeatureAsCorrelated; }
        }

        public string VM_SelectedGraphFeature
        {
            get { return model.SelectedGraphFeature; }
        }

        public string VM_SelectedGraphFeatureCorrelated
        {
            get { return model.SelectedGraphFeatureCorrelated; }
        }
        
        public List<DataPoint> VM_LinearRegression      
        {
            get { return model.LinearRegression; }
        }


        // COMMANDS
        public void LearnNormal()
        {
            model.LearnNormal();
        }

        public void setDllPath(string dllPath)
        {
            model.setDllPath(dllPath);
        }

        //public string GetMostCorrelatedFeatureOf(string feature)
        //{
        //    return model.GetMostCorrelatedFeatureOf(feature);
        //}

        public void updateSelectedFeature(string feature)
        {
            model.updateSelectedFeature(feature);
        }

        public void setGraphWindowOpened(bool value)
        {
            model.setGraphWindowOpened(value);
        }

        
    }
}
