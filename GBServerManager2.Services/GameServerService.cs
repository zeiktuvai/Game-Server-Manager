using GBServerManager2.Data;
using GBServerManager2.Models;
using GBServerManager2.Models.Enums;
using GBServerManager2.Services.Helpers;

namespace GBServerManager2.Services
{
    public class GameServerService
    {
        public ServerList List = new ServerList();
        private GameServerRepository _gsr { get; set; }

        public GameServerService(GameServerRepository gsr)
        {
            _gsr = gsr;
            UpdateGameServers();
        }

        public void UpdateGameServers()
        {
            List.Servers = _gsr.GetAllGameServers().ToList();
            List.Servers.ForEach(s => { s._PlayerStats = s._ServerPID != 0 ? SteamA2SHelper.A2S_INFO.GetServerStatistics(s) : "0/0"; });
        }
        
        public GameServer GenerateServerProcess(GameServer server, string serverBaseDir, ServerTypeEnum serverType)
        {            
            switch (serverType)
            {
                case 0:
                    var path = GBServerHelper.GetNewGBServerDirectory(serverBaseDir, server.ServerType);
                    (server as GBServer).ServerBasePath = path;
                    server.ServerWorkinDir = path;
                    server.serverProc = new GameServerProcess { proc = SteamCMDHelper.DownloadUpdateNewServer(server) };             
                    break;
            }

            return server;
        }

        public void AddNewGameServer(GameServer server, ServerTypeEnum serverType)
        {
            switch (serverType)
            {
                //TODO: Disabled initialize server stuff for speedy testing.
                case 0:
                    GBServer srv = server as GBServer;
                    srv.ServerPath = GBServerHelper.FindGBServerExecutable(srv);
                    //srv.InitialServerStart();
                    //GBServerHelper.CreateServerINIFile(srv);
                    _gsr.AddGameServer(srv, serverType);

                    break;
            }
                        
        }

        public bool AddExistingGameServer(string basePath, ServerTypeEnum serverType)
        {
            if (serverType == ServerTypeEnum.Ground_Branch)
            {
                var server = GBServerHelper.FindGBServerExecutable(basePath);
                if (!List.Servers.Any(s => s.ServerPath == server.ServerPath))
                {
                    server = GBServerHelper.RetrieveGBServerProperties(server);
                    server.ServerType = serverType;
                    _gsr.AddGameServer(server, serverType);
                    UpdateGameServers();
                    return true;
                }

                throw new Exception("Server exists in collection.");
            }
            return false;
        }
      
        public void UpdateServer(GameServer server)
        {
            //if (server != null)
            //{
            //    var ExistingServer = ServerCache._ServerList.Servers.Find(s => s.ServerId == server.ServerId);

            //    if (ExistingServer != null)
            //    {
            //        ExistingServer = server;
            //        JSONHelper.SaveServerToFile(ServerCache._ServerList);
            //    }
            //    else
            //    {
            //        throw new KeyNotFoundException("Server not found in collection.");
            //    }
            //}
        }

        public void DeleteServer(GameServer server)
        {
            _gsr.DeleteGameServer(server);
        }

        public Tuple<int, int> GetServerPorts()
        {
            return new Tuple<int, int>(List.Servers.Max(s => s.Port) + 1, List.Servers.Max(s => s.QueryPort) + 1);
        }

    }
}
