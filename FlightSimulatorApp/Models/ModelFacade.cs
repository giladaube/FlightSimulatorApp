using System.ComponentModel;

/***
 * ModelFacade- Facade for the models, a partial class so each individual model will be incharges for a
 * smaller porttion in the app so the each facade will be the model of the VMs.
 * The facade is breaked down to 4 smaller facades and the models only know the facade rather then having 
 * a conniction with each other.
 * The facade implement IModel which implement INotifyPropertyChanged to notify the relevant ViewModel.
 * The Models are:
 *      Opening the FG
 *      Opening client that connects to FG abd sending the data from the csv
 *      Keppeng track and changing the correct timestep acording to the user needs
 *      Calculating and displaying graphs
 *      Claculating anomalies using algorithm that can be changed
 * ***/

namespace FlightSimulatorApp.Models
{
    public partial class ModelFacade : IModel 
    {
        private csvParser parser;
        private string csvPath;

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
        partial void StartFacade_4();

        // Ends processes and threads.
        partial void EndFacade_1(); 
        partial void EndFacade_2(); 
        partial void EndFacade_3();
        partial void EndFacade_4();

        public ModelFacade()
        {
            //setCSVPath("C:\\Users\\gilad\\Downloads\\reg_flight.csv");
            //StartFacade_1();
            //StartFacade_2();
            //StartFacade_3();
        }

        private void Start()
        {
            StartFacade_1();
            StartFacade_2();
            StartFacade_3();
            //StartFacade_4();
        }

        public void end()
        {
            EndFacade_1();
            EndFacade_2();
            EndFacade_3();
            EndFacade_4();
        }

       
        public void setParserPath(string csvPath, string xmlPath)
        {
            this.csvPath = csvPath;
            parser = new csvParser(csvPath);
            parser.setXMLCol(xmlPath); // if xmlPath is null, parser a default XML file.
            parser.scanCsv();
            Start();
            this.NotifyPropertyChanged("timeSimulator");
        }
    }
}
