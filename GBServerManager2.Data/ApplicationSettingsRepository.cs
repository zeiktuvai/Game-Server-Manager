using GBServerManager2.Models;
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

        public ApplicationSettings GetApplicationSettings()
        {
            var coll = _db.GetCollection<ApplicationSettings>(GlobalConstants.AppSettingsCollectionName).FindAll();
            return coll.Any() ? coll.First() : null;
        }

        public int AddApplicationSettings(ApplicationSettings setting)
        {           
            return _db.GetCollection<ApplicationSettings>(GlobalConstants.AppSettingsCollectionName).Insert(setting);
        }

        public bool UpdateApplicationSettings(ApplicationSettings setting)
        {
            return _db.GetCollection<ApplicationSettings>(GlobalConstants.AppSettingsCollectionName).Update(setting);
        }
    }
}
