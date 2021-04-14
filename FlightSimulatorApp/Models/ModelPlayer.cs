using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

/***
 * PlayerModel- The model opens the flightgear app, having a client that connects to it and sending the
 * right data accordingly.
 * The model recives command from its VM on what data needs to be send.
 * ***/

namespace FlightSimulatorApp.Models
{
    class ModelPlayer : Notify
    {
        // Private fields
        private IClient client;
        private static Mutex mut = new Mutex();
        private Thread clientThread;
        private List<string> csvRows;
        volatile Boolean stop; // volatile indicates that the field might be modified by multiple threads

        // PROPERTIES

        // define intervals between each IClient.write's call.
        private int milliseconds;
        public int Milliseconds
        {
            get { return this.milliseconds; }
            set
            {
                if (this.milliseconds != value)
                {
                    this.milliseconds = value;
                }
            }
        }

        // Represent the current csvline to write.
        private int linestep;
        public int Linestep
        {
            get { return this.linestep; }
            set
            {
                if (this.linestep != value)
                {
                    this.linestep = value;
                    this.NotifyPropertyChanged("timestep");
                    this.NotifyPropertyChanged("timedisplay");
                }
            }
        }

        // Represent the current run-time of the simulation, in seconds.
        public double Timestep
        {
            get { return Convert.ToDouble(Linestep / 10); } // covert line into seconds.
        }


        // Constructor
        public ModelPlayer(string server, Int32 port, int linestep, List<string> rowsList)
        {
            this.client = new Client(); // use to transfer data to server via TCP.

            this.Linestep = linestep; // sends data from 'linestep' row.
            this.milliseconds = 100; // sends row in intervals of 'milliseconds'.
            this.stop = true;
            this.csvRows = rowsList;

            // this is the only access to start function.
            start(server, port); // connect client and initiate background process to send data.
        }

        private void start(string server, Int32 port)
        {
            ThreadStart clientThs = new ThreadStart(() =>
            {
                client.connect(server, port); // connect client.
                while (true) // make sure thread won't die in case of a pauseSimulator.
                {
                    while (!stop && Linestep < csvRows.Count - 1)
                    {
                        // Wait until it is safe to enter.
                        mut.WaitOne();

                        client.write(csvRows.ElementAt(Linestep)); // write line at Linestep to server.
                        Linestep++;
                        Thread.Sleep(Milliseconds); // wait Milliseconds' time before sending each line.

                        mut.ReleaseMutex(); // Release the Mutex.
                    }
                }
            });
            clientThread = new Thread(clientThs);
            clientThread.Start();
        }

        public void play()
        {
            this.stop = false; // start sending data. 
        }

        public void pause()
        {
            this.stop = true; // stop sending data. 
        }

        public void changeMilliseconds(int milliseconds)
        {
            this.Milliseconds = milliseconds;
        }

        public void changeLinestep(int linestep)
        {
            this.Linestep = linestep;
        }

        public void end()
        {
            client.disconnect();
            clientThread.Abort();
        }
        
    }
}
