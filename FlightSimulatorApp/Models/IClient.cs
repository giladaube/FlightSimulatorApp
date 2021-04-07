using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Models
{
    interface IClient
    {
        void connect(string server, Int32 port);

        void write(string command);

        void disconnect();
    }
}
