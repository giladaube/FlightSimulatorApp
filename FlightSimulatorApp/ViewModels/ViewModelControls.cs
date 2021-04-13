using FlightSimulatorApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/***
 * ViewModelControls- View Model, holds a the model notifying changes to the view and 
 * holds properties that are binded to view elements. 
 * This View Model holds a model in charged of the controls display.
 * ***/

namespace FlightSimulatorApp.ViewModels
{

    class ViewModelControls : Notify
    {
        // MEMBERS
        private IModel model;

        // PROPERTIES
        public double VM_Altimeter
        {
            get { return model.Altimeter * 0.38; }
        }

        public double VM_Airspeed
        {
            get { return model.Airspeed * 1.8; }
        }

        public double VM_Aileron
        {
            get { return model.Aileron * 100 + 125; }
        }

        public double VM_Elevator
        {
            get { return model.Elevator * 100 + 125; }
        }

        public double VM_Rudder
        {
            get { return model.Rudder; }
        }

        public double VM_Throttle
        {
            get { return model.Throttle; }
        }

        public double VM_Heading
        {
            get { return model.Heading; }
        }

        public double VM_Roll
        {
            get { return Math.Round(model.Roll, 4); }
        }

        public double VM_Pitch
        {
            get { return Math.Round(model.Pitch, 4); }
        }

        public double VM_Yaw
        {
            get { return Math.Round(model.Yaw, 4); }
        }

        // CONSTRUCTOR
        public ViewModelControls(IModel model)
        {
            this.model = model;
            model.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_" + e.PropertyName);
                };
        }

        // COMMANDS
        public void play()
        {
            model.play();
        }
    }
}
