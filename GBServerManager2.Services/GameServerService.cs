﻿using GBServerManager2.Data;
using GBServerManager2.Models;
using GBServerManager2.Models.Enums;
using GBServerManager2.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBServerManager2.Services
{
    public class GameServerService
    {
        public ServerList Servers = new ServerList();
        private GameServerRepository _gsr { get; set; }

        public GameServerService(GameServerRepository gsr)
        {
            _gsr = gsr;
            UpdateGameServers();
        }

        private void UpdateGameServers()
        {
            Servers.Servers = _gsr.GetAllGameServers().ToList();
        }

        public bool AddNewGameServer(string basePath, string serverExePath, ServerTypeEnum serverType)
        {
            if (serverType == ServerTypeEnum.Ground_Branch)
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

            }

            return false;            
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
