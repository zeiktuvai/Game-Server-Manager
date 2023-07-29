using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBServerManager2.Models
{
    public static class GlobalConstants
    {
        public static string AppSettingsCollectionName { get; private set; } = "ApplicationSettings";
        public static string GameServerCollectionName { get; private set; } = "GameServers";

        #region Settings Names
        public static string ServerBasePath { get; private set; } = "BasePath";
        #endregion

        public static string SteamCommandPath { get; private set; } = Path.Combine(Directory.GetCurrentDirectory(), "SteamCMD");
    }
}
