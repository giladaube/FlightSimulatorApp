using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Models
{
    class csvParser
    {
        public string path;
        public int numOfRows;
        public int numOfCols;
        public Dictionary<string, List<float>> dicAsCols;
        public Dictionary<float, List<float>> dicAsRows;
        public List<string> colNames;
        public List<string> rowsList;

        public csvParser(string path)
        {
            this.path = path;
            this.numOfRows = 0;
            this.numOfCols = 0;

            this.rowsList = new List<string>();
            this.dicAsCols = new Dictionary<string, List<float>>();
            this.dicAsRows = new Dictionary<float, List<float>>();

            colNames = new List<string>{"aileron",
    "elevator",
    "rudder",
    "flaps",
    "slats",
    "speedbrake",
    "throttle",
    "throttle1",
    "engine-pump",
    "engine-pump1",
    "electric-pump",
    "electric-pump1",
    "external-power",
    "APU-generator",
    "latitude-deg",
    "longitude-deg",
    "altitude-ft",
    "roll-deg",
    "pitch-deg",
    "heading-deg",
    "side-slip-deg",
    "airspeed-kt",
    "glideslope",
    "vertical-speed-fps",
    "airspeed-indicator_indicated-speed-kt",
    "altimeter_indicated-altitude-ft",
    "altimeter_pressure-alt-ft",
    "attitude-indicator_indicated-pitch-deg",
    "attitude-indicator_indicated-roll-deg",
    "attitude-indicator_internal-pitch-deg",
    "attitude-indicator_internal-roll-deg",
    "encoder_indicated-altitude-ft",
    "encoder_pressure-alt-ft",
    "gps_indicated-altitude-ft",
    "gps_indicated-ground-speed-kt",
    "gps_indicated-vertical-speed",
    "indicated-heading-deg",
    "magnetic-compass_indicated-heading-deg",
    "slip-skid-ball_indicated-slip-skid",
    "turn-indicator_indicated-turn-rate",
    "vertical-speed-indicator_indicated-speed-fpm",
    "engine_rpm" };
        }

        public void scanCsv()
        {
            // path to the csv file
            //string path = "C:/Users/Or/Documents/Uni/SemesterD/AdvProg2/reg_flight.csv";

            int i = 0;
            //if (File.Exists(path))
            //{
            string[] lines = System.IO.File.ReadAllLines(path);

            // Counting rows and cols
            foreach (string line in lines)
            {
                numOfRows++;
                this.rowsList.Add(line);
                string[] columns = line.Split(',');

                if (i == 0)
                {
                    foreach (string column in columns)
                    {
                        numOfCols++;
                    }
                }
                i = 1;
            }

            foreach (string name in this.colNames)
            {
                dicAsCols.Add(name, new List<float>());
            }

            int rowNum = 0, colNum = 0;
            foreach (string line in lines)
            {
                dicAsRows.Add(rowNum, new List<float>());
                string[] columns = line.Split(',');

                colNum = 0;
                foreach (string column in columns)
                {
                    // Convert value from string to float.
                    float colValueAsFloat = float.Parse(column);
                    // Add to row's float list.
                    dicAsRows.ElementAt(rowNum).Value.Add(colValueAsFloat);

                    //string colName = columnsNames.ElementAt(colNum).Value;
                    dicAsCols.ElementAt(colNum).Value.Add(float.Parse(column));
                    colNum++;
                }
                rowNum++;
            }
        }

        //private void scan()
        //{
        //    using (StreamReader sr = new StreamReader(path))
        //    {
        //        string currentLine;
        //        // currentLine will be null when the StreamReader reaches the end of file
        //        while ((currentLine = sr.ReadLine()) != null)
        //        {
        //            rowsList.Add(currentLine);
        //        }
        //    }
        //}
    }
}
