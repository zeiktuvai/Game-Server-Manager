using GBServerManager2.Models;
using GBServerManager2.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBServerManager2.Services
{
    public static class ServerService
    {
        public static ServerList GetGameServers()
        {
            //var _serverList = JSONHelper.ReadServersFromFile();
            //if (_serverList != null && _serverList.Servers != null)
            //{
            //    ServerCache._ServerList = _serverList;
            //    return _serverList;
            //}
            //else
            //{
            //    ServerCache._ServerList = new ServerList { Servers = new List<GBServer>() };
            //    return new ServerList() { Servers = new List<GBServer>() };
            //}
            return null;
        }

        public static GBServer AddGameServer(string basePath, string serverExePath)
        {
            //if (!string.IsNullOrEmpty(basePath) && !string.IsNullOrEmpty(serverExePath))
            //{
            //    var Server = GBServerService.RetrieveGBServerProperties(basePath, serverExePath);
            //    var ExistingServers = GetGBServers().Servers;

            //    if (!ExistingServers.Exists(s => s.ServerBasePath == basePath))
            //    {
            //        ServerCache._ServerList.Servers.Add(Server);
            //        JSONHelper.SaveServerToFile(ServerCache._ServerList);
            //    } 
            //    else
            //    {
            //        throw new Exception("Server has already been added");
            //    }
            //    return Server;
            //}

            return null;            
        }

        public static bool AddGBServer(GameServer server)
        {
            //ServerCache._ServerList.Servers.Add(server);
            //JSONHelper.SaveServerToFile(ServerCache._ServerList);
            return true;
        }
        
        public static void UpdateServer(GameServer server)
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


    }
}
