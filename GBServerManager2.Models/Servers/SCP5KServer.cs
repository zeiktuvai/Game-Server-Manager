using GameServerManager.Models.Enums;

namespace GameServerManager.Models.Servers
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
