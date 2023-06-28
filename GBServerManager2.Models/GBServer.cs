using GBServerManager2.Models.Enums;
using LiteDB;
using System.Diagnostics;
using System.Text;
using System.Text.Json.Serialization;

namespace GBServerManager2.Models
{
    public class GBServer : GameServer
    {
        public string Header { get; set; }
        public string ServerBasePath { get; set; }
        public string ServerMOTD { get; set; }
        public string ServerPassword { get; set; }
        public string SpectatorOnlyPassword { get; set; }
        public string MultiHome { get; set; }
        public int RestartTime { get; set; }
        public int MaxPlayers { get; set; }
        public int MaxSpectators { get; set; }
        public string GameRules { get; set; }
        [BsonIgnore]
        public string _Status { get; set; }
        [BsonIgnore]
        public string _PlayerStats { get; set; }
      

        public string GetProcessStartArgs()
        {
            return $"Multihome={this.MultiHome} Port={this.Port} QueryPort={this.QueryPort} ScheduledShutdownTime={this.RestartTime} -LOCALLOGTIMES - log";
        }

        public bool InitialServerStart()
        {
            try
            {
                var proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = base.ServerPath,
                        Arguments = GetProcessStartArgs()
                    }

                };
                var process = proc.Start();

                Thread.Sleep(10000);

                proc.Kill();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
