using GameServerManager.Models;
using GameServerManager.Models.Enums;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServerManager.Models.Servers
{
    public class GameServer
    {
        [BsonId]
        public int id { get; set; }
        public ServerTypeEnum ServerType { get; set; }
        public string ServerName { get; set; }
        public string ServerPath { get; set; }
        public string ServerBasePath { get; set; }
        public string ServerWorkinDir { get; set; }
        public int Port { get; set; }
        public int QueryPort { get; set; }
        public int MaxPlayers { get; set; }
        public string ServerPassword { get; set; }
        [BsonIgnore]
        public RunningProcess serverProc { get; set; }
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
            _IsPrivateServer = false;                        
        }
    }
}

