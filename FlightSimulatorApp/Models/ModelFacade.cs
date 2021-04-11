using System.ComponentModel;


namespace FlightSimulatorApp.Models
{
    public partial class ModelFacade : IModel 
    {
        private csvParser parser;

        // Notify
        public event PropertyChangedEventHandler PropertyChanged;
        private string csvPath;
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
