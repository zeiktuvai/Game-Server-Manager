using System.Diagnostics;
using System.Text;

namespace GBServerManager2.Services.Helpers
{
    public class DispatcherHelper
    {
        public StringBuilder processOutput { get; set; } = new StringBuilder();
        public string processOutputString { get; set; } = "";
        public delegate void procOutputUpdateHandler();
        public event procOutputUpdateHandler outputUpdated;
        private Process _proc { get; set; }

        public void StartProcess()
        {
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "C:\\GBS-Private\\GroundBranch\\Binaries\\Win64\\GroundBranchServer-Win64-Shipping.exe",
                    
                    WorkingDirectory = "C:\\GBS-Private\\GroundBranch\\Binaries\\Win64",
                    Arguments = "Multihome={0} Port={1} QueryPort={2} ScheduledShutdownTime={3} -LOCALLOGTIMES -log",
                    //Arguments = String.Format("Multihome={0} Port={1} QueryPort={2} ScheduledShutdownTime={3} -LOCALLOGTIMES -log", server.MultiHome, server.Port, server.QueryPort, server.RestartTime),
                    //WindowStyle = ProcessWindowStyle.Hidden,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                }

            };
            proc.OutputDataReceived += OutputRecieved;
            var process = proc.Start();

            if (process)
            {
                _proc = proc;
                proc.BeginOutputReadLine();
            }
            else
            {
                _proc = null;
            }

        }

        public void StopProcess()
        {
            _proc.Kill();
            processOutput.Clear();
            processOutputString = "";
        }

        public int GetProcID()
        {
            if (!_proc.HasExited)
            {
                return _proc.Id;
            }
            return 0;
        }

        private void OutputRecieved(object sendingProc, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                if(processOutput.Capacity == processOutput.MaxCapacity)
                {
                    processOutput.Clear();
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
                processOutput.Append($"</br> <span {css}>" + e.Data + "</span>");
                
                processOutputString = processOutput.ToString();
                outputUpdated?.Invoke();
            }

        }
        public static void RunServerProcessCheck(object sender, EventArgs e)
        {

            //foreach (var server in ServerCache._ServerList.Servers)
            //{
            //    if (server._ServerPID != 0)
            //    {
            //        var status = ProcessHelper.GetServerStatus(server._ServerPID);
            //        if (status == false)
            //        {
                        //if (!string.IsNullOrWhiteSpace(AppSettingsHelper.ReadSettings().SteamCMDPath))
                        //{
                        //    var proc = SteamCMDHelper.DownloadUpdateNewServer(server);
                        //    while (Process.GetProcessById(proc).HasExited != true)
                        //    {
                        //        Thread.Sleep(5000);
                        //    }
                        //}
                        //ProcessHelper.StartServer(server);
            //        }
            //    }
            //}
        }
    }
}
