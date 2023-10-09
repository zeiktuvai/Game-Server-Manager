using GameServerManager.Models.Enums;
using LiteDB;

namespace GameServerManager.Models.Servers
{
    public class OHDServer : GameServer
    {

        public OHDServer()
        {
            ServerType = ServerTypeEnum.Operation_Harsh_Doorstop;
            SteamAppId = "476400";
        }
    }
}
