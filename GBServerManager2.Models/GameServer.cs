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
        public Guid ServerId { get; set; }
        public ServerTypeEnum ServerType { get; set; }
        public string ServerName { get; set; }
        public string ServerPath { get; set; }
        public string ServerWorkinDir { get; set; }
        public int Port { get; set; }
        public int QueryPort { get; set; }                
        [BsonIgnore]
        public GameServerProcess serverProc { get; set; }
        [BsonIgnore]
        public int _ServerPID { get; set; }

        public GameServer()
        {
            serverProc = new GameServerProcess();
        }
    }

    public class GameServerProcess
    {
        public StringBuilder processOutput { get; set; } = new StringBuilder();
        public string processOutputString { get; set; } = "";
        private Process _proc { get; set; }
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
                _proc = proc;
                _proc.OutputDataReceived += OutputRecieved;
                proc.BeginOutputReadLine();

                return _proc.Id;
            }
            else
            {
                _proc = null;
                return 0;
            }

        }

        public void StopProcess()
        {
            this._proc.Kill();
            this.processOutput.Clear();
            this.processOutputString = "";
        }

        public int GetProcID()
        {
            if (this._proc.HasExited)
            {
                return this._proc.Id;
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
    }
}

