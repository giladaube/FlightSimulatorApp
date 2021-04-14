using FlightSimulatorApp.Models;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;

/***
 * ViewModelAnomalies- View Model, holds a the model and sending commands it had recived to the model,
 * notifying changes to the view and holds properties that are binded to view elements. 
 * This View Model holds a model in charged of the graphs display and the plugins.
 * ***/

namespace FlightSimulatorApp.ViewModels
{
    class ViewModelAnomalies : Notify
    {
        // MEMBERS
        private IModel model;

        // CONSTRUCTOR
        public ViewModelAnomalies(IModel model)
        {
            this.model = model;
            model.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_" + e.PropertyName);
                };
        }

        // PROPERTIES
        public List<string> VM_ColNames
        {
            get { return model.ColNames; }
        }


        public PlotModel VM_AnomalyPlotModel
        {
            get { return model.AnomalyPlotModel; }
        }


        public List<DataPoint> VM_AnomalousPointsList
        {
            get { return model.AnomalousPointsList; }
        }

        public List<Tuple<int, string>> VM_AnomaliesLinesteps
        {
            get 
            { 
                return model.AnomaliesLinesteps; 
            }
        }

        public List<string> VM_AnomaliesTimesteps
        {
            get
            {
                return model.AnomaliesTimesteps;
            }
        }


        // COMMANDS
        public void LoadDLL()
        {
            model.loadDLL();
        }

        public void updateSelectedAnomalyFeature(string feature)
        {
            model.updateSelectedAnomalyFeature(feature);
        }

        public void changeTimestepByString(string timestep)
        {
            model.changeTimestepByString(timestep);
        }
    }
}
