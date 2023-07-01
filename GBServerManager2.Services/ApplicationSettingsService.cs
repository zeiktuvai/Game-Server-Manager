using GBServerManager2.Data;
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

        public ApplicationSettings GetAllApplicationSettings()
        {
            return asr.GetApplicationSettings() ?? new ApplicationSettings() { ApplicationName = "Game Server Manager", Version = "1.0", 
                Settings = new List<AppSetting>()};
        }

    }
}
