using GameServerManager.Models.Enums;

namespace GameServerManager.Models.Options
{
    public class ApplicationSettings
    {
        public int Id { get; set; }
        public SettingsNameEnum SettingsGroupName { get; set; }
        public string Version { get; set; }
        public List<AppSetting> Settings { get; set; }
    }
}
