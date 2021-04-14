using System;
using System.ComponentModel;

namespace FlightSimulatorApp.Models
{
    public class SharedLinestep
    {
        private IModel model;
        public int Linestep
        {
            get { return model.Linestep; }
            set
            {
                model.Linestep = value;
            }
        }

        public SharedLinestep(IModel m)
        {
            model = m;
        }
    }

    public partial class ModelFacade 
    {
        // Models Private Member
        private ModelPlayer player;
        private SharedLinestep slinestep;

        // Initiate models here
        partial void StartFacade_1()
        {
            slinestep = new SharedLinestep(this);
            // add ModelPlayer and set it's Notify funcions.
            player = new ModelPlayer("localhost", 5400, 0, parser.rowsList);
            player.PropertyChanged +=
                    delegate (Object sender, PropertyChangedEventArgs e)
                    {
                        NotifyPropertyChanged(e.PropertyName);
                    };
        }

        // Ends Models, Processes and Threads.
        partial void EndFacade_1()
        {
            // End ModelPlayer.
            player.end();
        }

        public int Linestep
        {
            get { return player.Linestep; }
            set { player.changeLinestep(value); }
        }

        // Represent the current run-time of the simulation, in seconds.
        public double Timestep
        {
            get
            {
                return player.Timestep;
            }
            set
            {
                Linestep = Convert.ToInt32(value * 10);
                //player.changeLinestep(Convert.ToInt32(value * 10));
            }
        }

        // Represent the current run-time of the simulation as a string (HH:MM:SS).
        public string Timedisplay
        {
            get
            {
                int hh = (int)Timestep / 3600;
                int mm = (int)Timestep / 60;
                int ss = (int)Timestep - (hh * 3600) - (mm * 60);
                return String.Format("{0:00}:{1:00}:{2:00}", hh, mm, ss);
            }
        }

        // Represent the total time of the simulation.
        public int TimeSimulator
        {
            get { return (parser.numOfRows / 10); }
        }


        // ModelPlayer commands
        public void changeRate(double rate)
        {
            player.changeMilliseconds(Convert.ToInt32(100 / rate)); // convert from rate to milliseconds.
        }
        public void changeTimestep(double timestep)
        {
            Timestep = timestep;
        }
        public void playSimulator()
        {
            player.play();
            play();
        }
        public void pauseSimulator()
        {
            player.pause();
        }
        public void changeTimestepByString(string timestep)
        {
            foreach (Tuple<int, string> tuple in AnomaliesLinesteps)
            {
                if (tuple.Item2.Equals(timestep))
                {
                    changeTimestep(tuple.Item1);
                }
            }
        }
    }
}
