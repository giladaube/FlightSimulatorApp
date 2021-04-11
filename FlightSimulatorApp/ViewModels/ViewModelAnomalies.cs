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
    class ViewModelAnomalies : Notify
    {
        private IModel model;

        public ViewModelAnomalies(IModel model)
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

        public void LoadDLL()
        {
            model.loadDLL();
        }

        public void updateSelectedAnomalyFeature(string feature)
        {
            model.updateSelectedAnomalyFeature(feature);
        }
    }
}
