﻿using GBServerManager2.Models;
using System.Text;


namespace GBServerManager2.Services
{
    public static class GBServerHelper
    {
        public static GBServer FindGBServerExecutable(string path)
        {
            GBServer server = new GBServer();

            try
            {
                var dir = Directory.GetFiles(path, "GroundBranchServer*.exe", SearchOption.AllDirectories).ToList();

                if (dir != null && dir.Count > 0) 
                {
                    server.ServerPath = dir.Where(s => s.Contains("Win64")).First();
                    server.ServerBasePath = Path.GetDirectoryName(dir.Where(s => !s.Contains("Binaries")).First());
                    server.ServerWorkinDir = Path.GetDirectoryName(dir.Where(s => s.Contains("Win64")).First());
                }
                else
                {
                    throw new FileNotFoundException();
                }

                return server;
            }
            catch (Exception)
            {
                throw new FileNotFoundException();
            }
        }

        internal static GBServer RetrieveGBServerProperties(GBServer server)
        {            
            server.RestartTime = 12;

            try
            {
                var IniFiles = Directory.GetFiles(server.ServerBasePath, "Server.ini", SearchOption.AllDirectories);

                if (IniFiles != null && IniFiles.Length > 0)
                {
                    server = GetServerINIFile(server, IniFiles[0]);
                }
                server = GetGBServerStartOptions(server);
            }
            catch (Exception)
            {
                throw;
            }

            server.Header = server.ServerName.Substring(0, 15);            
            return server;
        }

        //internal static GBServer RetrieveGBServerProperties(GBServer server)
        //{
        //    var updateServer = server;
        //    updateServer.Header = server.ServerName.Length < 15 ? server.ServerName.Substring(0, server.ServerName.Length) : server.ServerName.Substring(0, 15);

        //    if (File.Exists(Path.Combine(server.ServerBasePath + "\\GroundBranch\\Binaries\\Win64\\GroundBranchServer-Win64-Shipping.exe")))
        //    {
        //        updateServer.ServerPath = Path.Combine(server.ServerBasePath + "\\GroundBranch\\Binaries\\Win64\\GroundBranchServer-Win64-Shipping.exe");
        //    }
        //    else
        //    {
        //        throw new FileNotFoundException();
        //    }

        //    return updateServer;

        //}

        internal static string GetNewGBServerDirectory()
        {
            //if (!string.IsNullOrWhiteSpace((AppSettingsHelper.ReadSettings()).ServerBasePath))
            //{
            //    var _basePath = AppSettingsHelper.ReadSettings().ServerBasePath;
            //    var _serverFolderName = "Server_";

            //    if (Directory.Exists(_basePath))
            //    {
            //        int dirNumber = 1;
            //        bool emptyDirFound = false;
            //        string? path = Path.Combine(_basePath, _serverFolderName + dirNumber);

            //        while (emptyDirFound == false)
            //        {
            //            var dirs = Directory.GetDirectories(_basePath);

            //            if (dirs.Length > 0)
            //            {
            //                foreach (var item in dirs)
            //                {
            //                    path = Path.Combine(_basePath, _serverFolderName + dirNumber);

            //                    if (item.Contains(path))
            //                    {
            //                        dirNumber++;
            //                    }
            //                }

            //                path = Path.Combine(_basePath, _serverFolderName + dirNumber);
            //                if (!Directory.Exists(path))
            //                {
            //                    emptyDirFound = true;
            //                }
            //            }
            //            else
            //            {
            //                emptyDirFound = true;
            //            }

            //        }
            //        return path;
            //    }
            //}

            return null;
        }

        internal static void CreateServerINIFile(GBServer server)
        {
            StringBuilder file = new StringBuilder();
            var INIPath = Path.Combine(server.ServerBasePath, "GroundBranch\\ServerConfig");

            file.AppendLine("[/Script/RBZooKeeper.ZKServer]");
            file.AppendLine(string.Format("ServerName={0}", server.ServerName));
            file.AppendLine(string.Format("ServerMOTD={0}", server.ServerMOTD));
            file.AppendLine(string.Format("MaxPlayers={0}", server.MaxPlayers));
            file.AppendLine(string.Format("MaxSpectators={0}", server.MaxSpectators));
            file.AppendLine(string.Format("GameRules={0}", server.GameRules));
            file.AppendLine(string.Format("ServerPassword={0}", server.ServerPassword));
            file.AppendLine(string.Format("SpectatorOnlyPassword={0}", server.SpectatorOnlyPassword));

            if (!Directory.Exists(INIPath))
            {
                Directory.CreateDirectory(INIPath);
            }
            File.WriteAllText(INIPath + "\\Server.ini", file.ToString());
        }

        internal static GBServer GetServerINIFile(GBServer server, string IniPath)
        {
           
            string ServerConfigFile = "";

            if (File.Exists(IniPath))
            {
                string _ReadFile = File.ReadAllText(IniPath);

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
                    if (item.Contains("ServerName="))
                    {
                        server.ServerName = item.Substring(item.IndexOf('=') + 1).Trim();
                    }
                    if (item.Contains("ServerMOTD="))
                    {
                        server.ServerMOTD = item.Substring(item.IndexOf('=') + 1).Trim();
                    }
                    if (item.Contains("MaxPlayers="))
                    {
                        string value = item.Substring(item.IndexOf('=') + 1);
                        server.MaxPlayers = string.IsNullOrEmpty(value) ? 0 : int.Parse(value);
                    }
                    if (item.Contains("MaxSpectators="))
                    {
                        string value = item.Substring(item.IndexOf('=') + 1);
                        server.MaxSpectators = string.IsNullOrEmpty(value) ? 0 : int.Parse(value);
                    }
                    if (item.Contains("GameRules="))
                    {
                        server.GameRules = item.Substring(item.IndexOf('=') + 1).Trim();
                    }
                    if (item.Contains("ServerPassword="))
                    {
                        server.ServerPassword = item.Substring(item.IndexOf('=') + 1).Trim();
                    }
                }

                return server;
            }
            else
            {
                throw new IOException("Failed to read Server.Ini file");
            }
        }

        internal static GBServer GetGBServerStartOptions(GBServer server)
        {
            string serverBatFile = "";
            var batFile = Directory.GetFiles(server.ServerBasePath, "DedicatedServer.bat", SearchOption.TopDirectoryOnly);
            int successfulCount = 0;

            if (batFile.Length > 0 && File.Exists(batFile[0]))
            {
                string readfile = File.ReadAllText(batFile[0]);

                if (!string.IsNullOrWhiteSpace(readfile))
                {
                    serverBatFile = readfile;
                }
            }

            if (!string.IsNullOrWhiteSpace(serverBatFile))
            {
                var File = serverBatFile.Split(Environment.NewLine);
                foreach (var item in File)
                {
                    if (successfulCount == 3)
                    {
                        break;
                    }
                    if (item.Contains("set MultiHome="))
                    {
                        server.MultiHome = item.Substring(item.IndexOf('=') + 1).Trim();
                        successfulCount++;
                    }
                    if (item.Contains("set Port="))
                    {
                        server.Port = string.IsNullOrEmpty(item.Substring(item.IndexOf("=") + 1).Trim()) ? 0 : int.Parse(item.Substring(item.IndexOf("=") + 1).Trim());
                        successfulCount++;
                    }
                    if (item.Contains("set QueryPort="))
                    {
                        server.QueryPort = string.IsNullOrEmpty(item.Substring(item.IndexOf("=") + 1).Trim()) ? 0 : int.Parse(item.Substring(item.IndexOf("=") + 1).Trim());
                        successfulCount++;
                    }

                }
            }

            return server;
        }
    }
}