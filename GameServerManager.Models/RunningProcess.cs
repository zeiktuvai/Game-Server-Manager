using GameServerManager.Models.Enums;
using GameServerManager.Models.Servers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GameServerManager.Models
{
    public class RunningProcess
    {
        public StringBuilder processOutput { get; set; } = new StringBuilder();
        public string processOutputString { get; set; } = "";
        public Process? proc { get; set; }        
        public delegate void procOutputUpdateHandler();        
        public event procOutputUpdateHandler outputUpdated;
        public delegate void procExitedHandler(object? sender, EventArgs e);
        public event procExitedHandler ProcessExited;

        public RunningProcess()
        {
            
        }

        public RunningProcess(Process _proc)
        {
            proc = _proc;
            proc.OutputDataReceived += OutputRecieved;
            proc.EnableRaisingEvents = true;
            proc.Exited += ProcExited;
            proc.BeginOutputReadLine();
        }


        private void OutputRecieved(object sendingProc, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                if (this.processOutput.Capacity == this.processOutput.MaxCapacity)
                {
                    this.processOutput.Clear();
                }

                string css = "";
                if (e.Data.Contains("Error"))
                {
                    css = "style=\"color: red\"";
                }
                if (e.Data.Contains("Warning"))
                {
                    css = "style=\"color: orange\"";
                }
                this.processOutput.Append($"</br> <span {css}>" + e.Data + "</span>");

                this.processOutputString = this.processOutput.ToString();
                outputUpdated?.Invoke();
            }
        }

        private void ProcExited(object? sender, EventArgs e)
        {
            ProcessExited?.Invoke(sender, e);
        }
    }
}
