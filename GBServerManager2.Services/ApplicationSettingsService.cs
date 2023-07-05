using GBServerManager2.Data;
using GBServerManager2.Models.Enums;
using GBServerManager2.Models.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBServerManager2.Services
{
    public class ApplicationSettingsService
    {
        private ApplicationSettingsRepository asr { get; set; }

        public ApplicationSettingsService(ApplicationSettingsRepository appSettingsRepo)
        {
            asr = appSettingsRepo;
        }

        public ApplicationSettings GetApplicationSettings()
        {
            return asr.GetSpecificApplicationSetting(SettingsNameEnum.ApplicationSettings) ?? 
                new ApplicationSettings() { SettingsGroupName = SettingsNameEnum.ApplicationSettings,
                Version = "1.0", Settings = new List<AppSetting>()};
        }

        public bool SaveApplicationSetting(ApplicationSettings setting)
        {
            try
            {
                if (!asr.UpdateApplicationSettings(setting))
                {
                    asr.AddApplicationSettings(setting);
                    return true;
                }
                return true;
            }
            catch (Exception)
            {
                return false;                
            }
        }

    }
}
