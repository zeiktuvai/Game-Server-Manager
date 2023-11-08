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
        public static ArmaServer GetServerProperties(ArmaServer server, string serverPath)
        {
            var path = FileHelper.FindFile(serverPath, "*.cfg");
            server.ConfigPath = FileHelper.GetFileRootPath(path);

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
                foreach (var item in configFile)
                {
                    if (item.ToLower().Contains("hostname"))
                    {
                        var text = item.Substring(item.IndexOf('=') + 3);
                        server.ServerName = text.Substring(0, text.IndexOf("\"")).Trim();
                    }                    
                    if (item.ToLower().Contains("maxplayers"))
                    {
                        string value = item.Substring(item.IndexOf('=') + 2);
                        var text = value.Substring(0, value.IndexOf(";")).Trim();
                        server.MaxPlayers = string.IsNullOrEmpty(text) ? 0 : int.Parse(text);
                    }                    
                    if (!item.ToLower().Contains("passwordadmin") && item.ToLower().Contains("password"))
                    {
                        var text = item.Substring(item.IndexOf('=') + 3);
                        server.ServerPassword = text.Substring(0, text.IndexOf(';') - 1).Trim();
                    }
                    if (item.ToLower().Contains("passwordadmin"))
                    {
                        var text = item.Substring(item.IndexOf('=') + 3);
                        server.AdminPassword = text.Substring(0, text.IndexOf(';') - 1).Trim();
                    }
                }

                return server;
            }
            else
            {
                throw new IOException("Failed to read server config file");
            }
        }
    }
}
