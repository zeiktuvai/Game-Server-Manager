using GBServerManager2.Models.Enums;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBServerManager2.Models
{
    public class SCP5KServer : GameServer
    {

        public SCP5KServer()
        {
            ServerType = ServerTypeEnum.SCP_5k;
            SteamAppId = "884110";
        }
    }
}
