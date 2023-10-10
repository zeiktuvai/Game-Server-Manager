using GameServerManager.Models.Servers;
using GameServerManager.Data;
using GameServerManager.Models;
using GameServerManager.Models.Enums;
using GameServerManager.Services.Helpers;
using GameServerManager.Models.Request;

namespace GameServerManager.Services
{
    public class GameServerService
    {
        public ServerList List = new ServerList();
        private GameServerRepository _gsr { get; set; }
        public SteamCMDService _scs { get; set; }

        public GameServerService(GameServerRepository gsr, SteamCMDService scs)
        {
            _gsr = gsr;
            _scs = scs;
            UpdateGameServers();
        }
        //TODO: Pull out process aspect into it's own singleton class
        public void UpdateGameServers()
        {
            List.Servers = _gsr.GetAllGameServers().ToList();
            List.Servers.ForEach(s => { s._PlayerStats = s._ServerPID != 0 ? SteamA2SHelper.A2S_INFO.GetServerStatistics(s) : "0/0"; });
        }
        
        //TODO: why am I doing it like this?
        public GameServer SendSteamCMDAction(GameServer server, string serverBaseDir, ServerTypeEnum serverType)
        {
            string? path;

            switch (serverType)
            {
                case ServerTypeEnum.Ground_Branch:
                    path = GetNewServerDirectory(serverBaseDir, server.ServerType);
                    (server as GBServer).ServerBasePath = path;
                    server.ServerWorkinDir = path;
                    server.serverProc = new GameServerProcess { proc = SteamCMDHelper.DownloadUpdateNewServer(server) };             
                    break;
                
                case ServerTypeEnum.Arma_3:
                    path = GetNewServerDirectory(serverBaseDir, server.ServerType);
                    (server as ArmaServer).ServerBasePath = path;
                    server.ServerWorkinDir = path;
                    server.serverProc = new GameServerProcess { proc = _scs.ExecuteSteamCMDRequest( new SteamCMDRequest
                    {
                        DownloadPath = path,
                        SteamAppId = (server as ArmaServer).SteamAppId,
                        AnonymousLogin = true,
                        SteamCMDPath = GlobalConstants.SteamCommandPath
                    })};
                    break;
            }

            return server;
        }

        public void AddNewGameServer(GameServer server, ServerTypeEnum serverType)
        {
            switch (serverType)
            {
                //TODO: Disabled initialize server stuff for speedy testing.
                case ServerTypeEnum.Ground_Branch:
                    GBServer srv = server as GBServer;
                    srv.ServerPath = GBServerHelper.FindGBServerExecutable(srv);
                    //srv.InitialServerStart();
                    //GBServerHelper.CreateServerINIFile(srv);
                    _gsr.AddGameServer(srv, serverType);
                    break;

                case ServerTypeEnum.Arma_3:
                    ArmaServer srvs = server as ArmaServer;
                    
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






        private string GetNewServerDirectory(string ServerBasePath, ServerTypeEnum ServerType)
        {
            if (!string.IsNullOrWhiteSpace(ServerBasePath))
            {
                var _serverFolderName = "";

                switch (ServerType)
                {
                    case ServerTypeEnum.Ground_Branch:
                        _serverFolderName = "GBServer_";
                        break;
                    case ServerTypeEnum.Operation_Harsh_Doorstop:
                        _serverFolderName = "OHDServer_";
                        break;
                    case ServerTypeEnum.SCP_5k:
                        _serverFolderName = "SCPServer_";
                        break;
                    case ServerTypeEnum.Arma_3:
                        _serverFolderName = "ArmaServer_";
                        break;
                }


                if (Directory.Exists(ServerBasePath))
                {
                    int dirNumber = 1;
                    bool emptyDirFound = false;
                    string? path = Path.Combine(ServerBasePath, _serverFolderName + dirNumber);

                    while (emptyDirFound == false)
                    {
                        var dirs = Directory.GetDirectories(ServerBasePath);

                        if (dirs.Length > 0)
                        {
                            foreach (var item in dirs)
                            {
                                path = Path.Combine(ServerBasePath, _serverFolderName + dirNumber);

                                if (item.Contains(path))
                                {
                                    dirNumber++;
                                }
                            }

                            path = Path.Combine(ServerBasePath, _serverFolderName + dirNumber);
                            if (!Directory.Exists(path))
                            {
                                emptyDirFound = true;
                            }
                        }
                        else
                        {
                            emptyDirFound = true;
                        }

                    }
                    return path;
                }
            }

            return null;
        }
    }
}
