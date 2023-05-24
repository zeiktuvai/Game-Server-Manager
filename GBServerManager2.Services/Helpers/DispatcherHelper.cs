﻿using System.Diagnostics;
using System.Text;

namespace GBServerManager2.Services.Helpers
{
    public class DispatcherHelper
    {
        public StringBuilder processOutput { get; set; } = new StringBuilder();
        private Process _proc { get; set; }

        public void StartProcess()
        {
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "C:\\Windows\\System32\\cmd.exe",
                    WorkingDirectory = "C:\\Windows\\System32",
                    Arguments = "/k ipconfig",
                    //Arguments = String.Format("Multihome={0} Port={1} QueryPort={2} ScheduledShutdownTime={3} -LOCALLOGTIMES -log", server.MultiHome, server.Port, server.QueryPort, server.RestartTime),
                    //WindowStyle = ProcessWindowStyle.Hidden,
                    UseShellExecute = false,
                    RedirectStandardOutput = true                    
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


        public void OutputRecieved(object sendingProc, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                processOutput.Append(Environment.NewLine + e.Data);
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
