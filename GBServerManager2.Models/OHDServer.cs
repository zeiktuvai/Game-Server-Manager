using GBServerManager2.Models.Enums;
using LiteDB;

namespace GBServerManager2.Models
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
