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
        public static ServerList GetGBServers()
        {
            var _serverList = JSONHelper.ReadServersFromFile();
            if (_serverList != null && _serverList.Servers != null)
            {
                ServerCache._ServerList = _serverList;
                return _serverList;
            }
            else
            {
                ServerCache._ServerList = new ServerList { Servers = new List<GBServer>() };
                return new ServerList() { Servers = new List<GBServer>() };
            }
        }

        public static GBServer AddGBServer(string basePath, string serverExePath)
        {
            if (!string.IsNullOrEmpty(basePath) && !string.IsNullOrEmpty(serverExePath))
            {
                var Server = GameServerService.RetrieveGBServerProperties(basePath, serverExePath);
                var ExistingServers = GetGBServers().Servers;

                if (!ExistingServers.Exists(s => s.ServerBasePath == basePath))
                {
                    ServerCache._ServerList.Servers.Add(Server);
                    JSONHelper.SaveServerToFile(ServerCache._ServerList);
                } 
                else
                {
                    throw new Exception("Server has already been added");
                }
                return Server;
            }

            return null;            
        }

        public static bool AddGBServer(GBServer server)
        {
            ServerCache._ServerList.Servers.Add(server);
            JSONHelper.SaveServerToFile(ServerCache._ServerList);
            return true;
        }
        
        public static void UpdateGBServer(GBServer server)
        {
            if (server != null)
            {
                var ExistingServer = ServerCache._ServerList.Servers.Find(s => s.ServerId == server.ServerId);

                if (ExistingServer != null)
                {
                    ExistingServer = server;
                    JSONHelper.SaveServerToFile(ServerCache._ServerList);
                }
                else
                {
                    throw new KeyNotFoundException("Server not found in collection.");
                }
            }
        }


    }
}
