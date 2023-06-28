using GBServerManager2.Models.Options;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBServerManager2.Data
{
    public class ApplicationSettingsRepository
    {
        private LiteDatabase _db;

        public ApplicationSettingsRepository(LiteDbContext dbContext)
        {
            _db = dbContext.Database;
        }

        public IEnumerable<AppSetting> GetApplicationSettings()
        {
            var result = _db.GetCollection<AppSetting>("ApplicationSettings").FindAll();
            return result;
        }

        public int saveApplicationSetting(AppSetting setting)
        {
            return _db.GetCollection<AppSetting>("ApplicationSettings").Insert(setting);
        }
    }
}
