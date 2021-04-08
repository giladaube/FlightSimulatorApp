using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

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
            this.colNames = new List<string>();
        }

        public void setXMLCol(string path)
        {
            XmlDocument doc = new XmlDocument();
            // in case no XMLPath was given, use a default XML
            if (path != null)
            {
                doc.Load(path);
            }
            else
            {
                doc.LoadXml(Properties.Resources.playback_small); // default XML file
            }

            XmlNodeList elemList = doc.GetElementsByTagName("name");
            for (int i = 0; i < elemList.Count / 2; i++)
            {
                colNames.Add(elemList[i].InnerXml.ToString());
            }
        }

        public void scanCsv()
        {
            int i = 0;
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
                if (dicAsCols.ContainsKey(name)) // in case of a double named column
                {
                    dicAsCols.Add(name + "[1]", new List<float>()); 
                }
                else
                {
                    dicAsCols.Add(name, new List<float>());
                }
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
    }
}
