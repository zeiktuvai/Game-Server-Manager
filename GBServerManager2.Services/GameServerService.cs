using GBServerManager2.Models;
using System.Text;


namespace GBServerManager2.Services
{
    public static class GameServerService
    {
        public static Server FindGBServerExecutable(string FileName)
        {
            Server paths = new Server();
            string GBFolderName = "GroundBranch";

            try
            {
                string DirPath = Path.GetDirectoryName(FileName);

                if (FileName.Contains("GroundBranchServer-Win64-Shipping.exe"))
                {
                    string path = Path.GetDirectoryName(FileName);
                    paths.ServerBasePath = path.Substring(0, path.IndexOf(GBFolderName));
                    paths.ServerPath = FileName;
                }

                if (FileName.Contains("GroundBranchServer.exe"))
                {
                    paths.ServerBasePath = Path.GetDirectoryName(FileName);
                    var exeFiles = Directory.GetFiles(paths.ServerBasePath, "*.exe", SearchOption.AllDirectories);
                    foreach (var item in exeFiles)
                    {
                        if (item.Contains("GroundBranchServer-Win64-Shipping.exe"))
                        {
                            paths.ServerPath = item;
                        }
                    }
                }

                return paths;
            }
            catch (Exception)
            {
                throw new FileNotFoundException();
            }
        }

        internal static Server RetrieveGBServerProperties(string BasePath, string ServerExePath)
        {
            string ServerIniPath = "";
            Server NewServer = new Server();

            NewServer.ServerId = Guid.NewGuid();
            NewServer.ServerBasePath = BasePath;
            NewServer.ServerPath = ServerExePath;
            NewServer.RestartTime = 24;

            try
            {
                var IniFiles = Directory.GetFiles(BasePath, "*.ini", SearchOption.AllDirectories);

                foreach (var File in IniFiles)
                {
                    if (File.Contains("Server.ini"))
                    {
                        ServerIniPath = File;
                        break;
                    }
                }

                NewServer = GetServerINIFile(NewServer);

            }
            catch (Exception)
            {

                throw;
            }

            NewServer.Header = NewServer.ServerName.Substring(0, 15);
            return NewServer;
        }

        internal static Server RetrieveGBServerProperties(Server server)
        {
            var updateServer = server;
            updateServer.Header = server.ServerName.Length < 15 ? server.ServerName.Substring(0, server.ServerName.Length) : server.ServerName.Substring(0, 15);

            if (File.Exists(Path.Combine(server.ServerBasePath + "\\GroundBranch\\Binaries\\Win64\\GroundBranchServer-Win64-Shipping.exe")))
            {
                updateServer.ServerPath = Path.Combine(server.ServerBasePath + "\\GroundBranch\\Binaries\\Win64\\GroundBranchServer-Win64-Shipping.exe");
            }
            else
            {
                throw new FileNotFoundException();
            }

            return updateServer;

        }

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

        internal static void CreateServerINIFile(Server server)
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

        internal static Server GetServerINIFile(Server server)
        {
            var ServerIniPath = Path.Combine(server.ServerBasePath, "GroundBranch\\ServerConfig\\Server.ini");
            string ServerConfigFile = "";

            if (File.Exists(ServerIniPath))
            {
                string _ReadFile = File.ReadAllText(ServerIniPath);

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
    }
}