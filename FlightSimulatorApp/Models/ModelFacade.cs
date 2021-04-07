using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Models
{
    public partial class ModelFacade : IModel 
    {
        private csvParser parser;

        // Notify
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        // Creates models
        partial void StartFacade_1(); 
        partial void StartFacade_2();
        partial void StartFacade_3();

        // Ends processes and threads.
        partial void EndFacade_1(); 
        partial void EndFacade_2(); 
        partial void EndFacade_3(); 

        public ModelFacade()
        {
            StartFacade_1();
            StartFacade_2();
            StartFacade_3();
        }

        public void end()
        {
            EndFacade_1();
            EndFacade_2();
            EndFacade_3();
        }

        public void setCSVPath(string path)
        {
            parser = new csvParser(path);
            parser.scanCsv();
            this.NotifyPropertyChanged("timeSimulator");
        }

    }
}
