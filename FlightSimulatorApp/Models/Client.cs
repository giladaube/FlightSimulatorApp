using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Models
{
    class Client : IClient
    {
        private TcpClient tcpClient;
        private StreamWriter srClient;

        public void connect(string server, int port)
        {
            // Create a TCP/IP Client.  
            this.tcpClient = new TcpClient();

            Boolean connectionEstablished = false;
            while (!connectionEstablished) // continues tries to create a connection.
            {
                try
                {
                    this.tcpClient.Connect(server, port); // connect to the specific server and port.
                    this.srClient = new StreamWriter(tcpClient.GetStream()); // uses to send data in write() function.

                    connectionEstablished = true; // happen only when no errors occur.
                    Debug.WriteLine("TCPCLIENT CONNECTED");
                }
                catch (Exception e)
                {
                    Debug.WriteLine("TCPCLIENT NOT CONNECTED");
                }
            }
        }

        public void write(string command)
        {
            // Send the data through StreamWrite.
            this.srClient.Write(command + "\n");
            Debug.WriteLine("send line: {s}", command);
        }

        public void disconnect()
        {
            if (tcpClient != null) // make sure tcpClient is connected.
            {
                if (srClient != null)
                    srClient.Close(); // ends StreamWrite.
                tcpClient.Close(); // ends TcpClient.
            }
        }
    }
}
