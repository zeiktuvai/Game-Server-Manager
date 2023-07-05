using GBServerManager2.Models.Enums;

namespace GBServerManager2.Models.Options
{
    public class ApplicationSettings
    {
        public int Id { get; set; }
        public SettingsNameEnum SettingsGroupName { get; set; }
        public string Version { get; set; }
        public List<AppSetting> Settings { get; set; }
    }
}
