using GameServerManager.Models.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServerManager.Services.Helpers
{
    public static class ArmaServerHelper
    {
        public static void GetServerProperties(string serverPath)
        {
            var path = FileHelper.FindFile(serverPath, "*.cfg");

            string ServerConfigFile = "";

            if (!string.IsNullOrWhiteSpace(path))
            {
                string _ReadFile = File.ReadAllText(path);

                if (!string.IsNullOrEmpty(_ReadFile))
                {
                    ServerConfigFile = _ReadFile;
                }
            }

            if (!string.IsNullOrEmpty(ServerConfigFile))
            {
                var configFile = ServerConfigFile.Split(Environment.NewLine);
                //foreach (var item in configFile)
                //{
                //    if (item.Contains("ServerName="))
                //    {
                //        server.ServerName = item.Substring(item.IndexOf('=') + 1).Trim();
                //    }
                //    if (item.Contains("ServerMOTD="))
                //    {
                //        server.ServerMOTD = item.Substring(item.IndexOf('=') + 1).Trim();
                //    }
                //    if (item.Contains("MaxPlayers="))
                //    {
                //        string value = item.Substring(item.IndexOf('=') + 1);
                //        server.MaxPlayers = string.IsNullOrEmpty(value) ? 0 : int.Parse(value);
                //    }
                //    if (item.Contains("MaxSpectators="))
                //    {
                //        string value = item.Substring(item.IndexOf('=') + 1);
                //        server.MaxSpectators = string.IsNullOrEmpty(value) ? 0 : int.Parse(value);
                //    }
                //    if (item.Contains("GameRules="))
                //    {
                //        server.GameRules = item.Substring(item.IndexOf('=') + 1).Trim();
                //    }
                //    if (item.Contains("ServerPassword="))
                //    {
                //        server.ServerPassword = item.Substring(item.IndexOf('=') + 1).Trim();
                //    }
                //}

                //return server;
            }
            else
            {
                throw new IOException("Failed to read server config file");
            }
        }
    }
}
