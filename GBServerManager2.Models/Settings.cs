namespace GameServerManager.Models
{
    internal class Settings
    {
        public string SteamCMDPath { get; set; }
        public string ServerBasePath { get; set; }
        public int defServerRestart { get; set; } = 12;

    }
}
