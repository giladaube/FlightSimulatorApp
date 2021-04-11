using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace FlightSimulatorApp.Models
{
    public partial class ModelFacade
    {
        // Models Private Member
        private double altimeter;
        private double airspeed;
        private double aileron;
        private double elevator;
        private double rudder;
        private double throttle;
        private double heading;
        private double roll;
        private double pitch;
        private double yaw;


        private bool stop;
        private SharedLinestep ls;


        // Initiate models here
        partial void StartFacade_2()
        {
            // add a new model here and set it's Notify funcions.
            ls = new SharedLinestep(this);
            this.stop = false;
            player.PropertyChanged +=
                    delegate (Object sender, PropertyChangedEventArgs e)
                    {
                        NotifyPropertyChanged(e.PropertyName);
                    };



        }


        public void play()
        {
            new Thread(delegate () {
                while (!stop)
                {
                    Altimeter = this.parser.dicAsCols["altimeter_indicated-altitude-ft"][ls.Linestep];
                    Airspeed = this.parser.dicAsCols["airspeed-kt"][ls.Linestep];
                    Aileron = this.parser.dicAsCols["aileron"][ls.Linestep];
                    Elevator = this.parser.dicAsCols["elevator"][ls.Linestep];
                    Rudder = this.parser.dicAsCols["rudder"][ls.Linestep];
                    Throttle = this.parser.dicAsCols["throttle"][ls.Linestep];
                    Heading = this.parser.dicAsCols["heading-deg"][ls.Linestep];
                    Roll = this.parser.dicAsCols["roll-deg"][ls.Linestep];
                    Pitch = this.parser.dicAsCols["pitch-deg"][ls.Linestep];
                    Yaw = this.parser.dicAsCols["side-slip-deg"][ls.Linestep];
                    Thread.Sleep(50);
                }
            }).Start();
        }

        // Ends Models, Processes and Threads.
        partial void EndFacade_2()
        {
            this.stop = true;
        }

        // Models Properies
        public double Altimeter
        {
            get { return altimeter; }
            set
            {
                if (value != altimeter)
                {
                    altimeter = value;
                    NotifyPropertyChanged("Altimeter");
                }

            }
        }

        public double Airspeed
        {
            get { return airspeed; }
            set
            {
                if (value != airspeed)
                {
                    airspeed = value;
                    NotifyPropertyChanged("Airspeed");
                }
            }
        }

        public double Elevator
        {
            get { return elevator; }
            set
            {
                if (value != elevator)
                {
                    elevator = value;
                    NotifyPropertyChanged("Elevator");
                }

            }
        }
        public double Aileron
        {
            get { return aileron; }
            set
            {
                if (value != aileron)
                {
                    aileron = value;
                    NotifyPropertyChanged("Aileron");
                }

            }
        }

        public double Rudder
        {
            get { return rudder; }
            set
            {
                if (value != rudder)
                {
                    rudder = value;
                    NotifyPropertyChanged("Rudder");
                }

            }
        }

        public double Throttle
        {
            get { return throttle; }
            set
            {
                if (value != throttle)
                {
                    throttle = value;
                    NotifyPropertyChanged("Throttle");
                }
            }
        }

        public double Heading
        {
            get { return heading; }
            set
            {
                if (value != heading)
                {
                    heading = value;
                    NotifyPropertyChanged("Heading");
                }

            }
        }

        public double Roll
        {
            get { return roll; }
            set
            {
                if (value != roll)
                {
                    roll = value;
                    NotifyPropertyChanged("Roll");
                }

            }
        }

        public double Pitch
        {
            get { return pitch; }
            set
            {
                if (value != pitch)
                {
                    pitch = value;
                    NotifyPropertyChanged("Pitch");
                }

            }
        }

        public double Yaw
        {
            get { return yaw; }
            set
            {
                if (value != yaw)
                {
                    yaw = value;
                    NotifyPropertyChanged("Yaw");
                }

            }
        }
    }

}

