using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServerManager.Models.Servers
{
    public class ArmaServer : GameServer
    {
        public string ProfilePath { get; set; }
        public string BattleEyePath { get; set; }
        public string ConfigPath { get; set; }
        public string ServerIP { get; set; }
        public string ServerWorld { get; set; }
        public int CPUCount { get; set; }
        public int Threads { get; set; }
        public int BandwidthAlgorithm { get; set; }
        public int MaxMemory { get; set; }
        public IEnumerable<string> ModList { get; set; }

        public ArmaServer()
        {
            ServerType = Enums.ServerTypeEnum.Arma_3;
            SteamAppId = "233780";
        }
    }
}
