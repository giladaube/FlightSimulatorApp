using FlightSimulatorApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/***
 * ViewModelPlayer- View Model, holds a the model and sending commands it had recived to the model,
 * notifying changes from to view and holds properties that are binded to view elements. 
 * This View Model holds a model in charged of the player features.
 * ***/

namespace FlightSimulatorApp.ViewModels
{
    class ViewModelPlayer : Notify
    {
        // MEMBERS
        private IModel model;

        // PROPERTIES
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

        public int VM_timeSimulator
        {
            get { return model.TimeSimulator; }
        }


        // CONTRUCTOR
        public ViewModelPlayer(IModel model)
        {
            this.model = model;
            model.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_" + e.PropertyName);
                };
        }

        // COMMANDS
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
