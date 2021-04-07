using FlightSimulatorApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.ViewModels
{
    class ViewModelPlayer : Notify
    {
        private IModel model;

        public double VM_timestep // value sends from View, so it's in seconds.
        {
            get
            {
                return model.Timestep;
            }
            set
            {
                model.Timestep = value;
            }
        }

        public string VM_timedisplay
        {
            get
            {
                return model.Timedisplay;
            }
        }

        public ViewModelPlayer(IModel model)
        {
            this.model = model;
            model.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_" + e.PropertyName);
                };
        }

        public void changeRate(double rate)
        {
            model.changeRate(rate);
        }
        
        public void changeTimestep(double timestep)
        {
            model.changeTimestep(timestep);
        }

        public void play()
        {
            model.playSimulator();
        }

        public void pause()
        {
            model.pauseSimulator();
        }

        public void close()
        {
            model.end();
        }
    }
}
