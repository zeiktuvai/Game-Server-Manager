using GameServerManager.Models.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServerManager.Models.Request
{
    public class SteamCMDRequest
    {
        private string _appPath;

        public string SteamCMDPath
        {
            get { return _appPath; }
            set { _appPath = value + "\\steamcmd.exe"; }
        }

        public string DownloadPath { get; set; }
        public string SteamAppId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string SteamGuardCode { get; set; }
        public bool AnonymousLogin { get; set; }
        public bool InitialRun { get; set; } = false;

        public string GetSteamCMDArguments()
        {
            if (InitialRun)
            {
                return "+quit";
            }
            else
            {
                string login = AnonymousLogin == true ? "anonymous" : $"{UserName} {Password} {SteamGuardCode}";
                return $"+force_install_dir \"{DownloadPath}\" +login {login} +app_update {SteamAppId} +quit";
            }
        }
    }
}
