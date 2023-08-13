using GBServerManager2.Models;
using GBServerManager2.Models.Enums;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBServerManager2.Models
{
    public class GameServer
    {
        [BsonId]
        public int id { get; set; }
        public ServerTypeEnum ServerType { get; set; }
        public string ServerName { get; set; }
        public string ServerPath { get; set; }
        public string ServerWorkinDir { get; set; }
        public int Port { get; set; }
        public int QueryPort { get; set; }
        public int MaxPlayers { get; set; }
        public string ServerPassword { get; set; }
        [BsonIgnore]
        public GameServerProcess serverProc { get; set; }
        [BsonIgnore]
        public int _ServerPID { get; set; }
        [BsonIgnore]
        public string _PlayerStats { get; set; }
        [BsonIgnore]
        public bool _IsPrivateServer { get; set; }
        [BsonIgnore]
        public string SteamAppId { get; init; }

        public GameServer()
        {
            serverProc = new GameServerProcess();
            _IsPrivateServer = false;                        
        }
    }

    public class GameServerProcess
    {
        public StringBuilder processOutput { get; set; } = new StringBuilder();
        public string processOutputString { get; set; } = "";
        public Process proc { get; set; }
        public delegate void procOutputUpdateHandler();
        public event procOutputUpdateHandler outputUpdated;

        /// <summary>
        /// Tries to start a game server.  Returns PID if successful, 0 if failed.
        /// </summary>
        /// <param name="server"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public int StartServerProcess(GameServer server, string args)
        {
            //if (!string.IsNullOrEmpty(server.MultiHome)
            //    & server.Port != 0
            //    & server.QueryPort != 0
            //    & server.RestartTime != 0)

                var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = server.ServerPath,
                    WorkingDirectory = server.ServerWorkinDir,
                    Arguments = args,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                }

            };
            var process = proc.Start();

            if (process)
            {
                this.proc = proc;
                this.proc.OutputDataReceived += OutputRecieved;
                proc.BeginOutputReadLine();

                return this.proc.Id;
            }
            else
            {
                this.proc = null;
                return 0;
            }

        }

        public void StopProcess()
        {
            this.proc.Kill();
            this.processOutput.Clear();
            this.processOutputString = "";
        }

        public int GetProcID()
        {
            if (this.proc.HasExited)
            {
                return this.proc.Id;
            }
            return 0;
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

        public static void RunServerProcessCheck(ServerList servers)
        {
            foreach (var server in servers.Servers)
            {
                if (server._ServerPID != 0)
                {
                    var status = Process.GetProcessById(server._ServerPID)?.HasExited ?? false;

                    if (status == false)
                    {
                        //if (!string.IsNullOrWhiteSpace(AppSettingsHelper.ReadSettings().SteamCMDPath))
                        //{
                        //    var proc = SteamCMDHelper.DownloadUpdateNewServer(server);
                        //    while (Process.GetProcessById(proc).HasExited != true)
                        //    {
                        //        Thread.Sleep(5000);
                        //    }
                        //}
                        //ProcessHelper.StartServer(server);
                    }
                }
            }
        }

        /// <summary>
        /// Checks the provided PID to see if a process is running under that PID.
        /// </summary>
        /// <param name="serverPID"></param>
        /// <returns>Returns True if the process is running, otherwise it returns False.</returns>
        public static bool GetServerProcessStatus(int serverPID)
        {
            Process proc = null;

            try
            {
                proc = Process.GetProcessById(serverPID);
            }
            catch (Exception)
            {
                return false;
            }


            if (proc != null && !proc.HasExited)
            {
                return true;
            }
            else
            {
                return false;
            }
        }               
    }
}

