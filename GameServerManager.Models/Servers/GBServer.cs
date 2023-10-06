using GameServerManager.Models.Enums;
using LiteDB;
using System.Diagnostics;

namespace GameServerManager.Models.Servers
{
    public class GBServer : GameServer
    {
        public string ServerBasePath { get; set; }
        public string ServerMOTD { get; set; }
        public string SpectatorOnlyPassword { get; set; }
        public string MultiHome { get; set; }
        public int RestartTime { get; set; }
        public int MaxSpectators { get; set; }
        public string GameRules { get; set; }
        [BsonIgnore]
        public string _Status { get; set; }


        public GBServer()
        {
            ServerType = ServerTypeEnum.Ground_Branch;
            MultiHome = "0.0.0.0";
            MaxPlayers = 16;
            RestartTime = 12;
            SteamAppId = "476400";
        }

        public string GetProcessStartArgs()
        {
            return $"Multihome={MultiHome} Port={Port} QueryPort={QueryPort} ScheduledShutdownTime={RestartTime} -LOCALLOGTIMES - log";
        }

        public bool InitialServerStart()
        {
            try
            {
                var proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = ServerPath,
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
