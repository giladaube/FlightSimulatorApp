using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace FlightSimulatorApp
{
    class FG
    {
        private Process fgProcess; // FG process id.
        private Boolean needToClose = false;
        private string path;
        private string xml;

        public FG()
        {
            this.fgProcess = new Process(); // create a new Process.
            path = MainWindow.FgPath;
            xml = MainWindow.XmlName;


        }

        public void start()
        {

            string wd = path + "\\data";
            string fn = path + "\\bin\\fgfs.exe";
            string arg = "--generic=socket,in,10,127.0.0.1,5400,tcp," + xml + " --fdm=null";

            needToClose = true;
            this.fgProcess.StartInfo.RedirectStandardOutput = true;
            this.fgProcess.StartInfo.UseShellExecute = false;
            this.fgProcess.StartInfo.CreateNoWindow = true;
            this.fgProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            this.fgProcess.StartInfo.WorkingDirectory = wd; // set working directory for FG.
            this.fgProcess.StartInfo.FileName = fn; // set process to open FG.
            this.fgProcess.StartInfo.Arguments = arg; // set FG to listen on port 5400.

            //this.fgProcess.StartInfo.WorkingDirectory = "C:\\Program Files\\FlightGear 2020.3.6\\data"; // set working directory for FG.
            //this.fgProcess.StartInfo.FileName = "c:\\program files\\flightgear 2020.3.6\\bin\\fgfs.exe"; // set process to open FG.
            //this.fgProcess.StartInfo.Arguments = "--generic=socket,in,10,127.0.0.1,5400,tcp,playback_small --fdm=null"; // set FG to listen on port 5400.
            
            ThreadStart fgThs = new ThreadStart(() => this.fgProcess.Start()); // set thread to start process.
            Thread fgThread = new Thread(fgThs);
            fgThread.Start(); // execute thread and start FG as another process.  
        }

        public void close()
        {
            if (needToClose && this.fgProcess != null && !fgProcess.HasExited) // FG process still running.
            {
                this.fgProcess.CloseMainWindow(); // stops FG process.
                this.fgProcess.Close(); // clear all used resources.
            }
        }

        private const int APPCOMMAND_VOLUME_MUTE = 0x80000;
        private const int WM_APPCOMMAND = 0x319;

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessageW(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        public void soundHandle()
        {
            SendMessageW(fgProcess.MainWindowHandle, WM_APPCOMMAND, fgProcess.MainWindowHandle, (IntPtr)APPCOMMAND_VOLUME_MUTE);
        }
    }
}
