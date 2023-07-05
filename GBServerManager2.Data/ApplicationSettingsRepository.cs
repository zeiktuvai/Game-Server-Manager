using GBServerManager2.Models;
using GBServerManager2.Models.Enums;
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

        public ApplicationSettings GetSpecificApplicationSetting(SettingsNameEnum name)
        {
            var coll = _db.GetCollection<ApplicationSettings>(GlobalConstants.AppSettingsCollectionName).FindOne(c => c.SettingsGroupName == name);
            return coll;
        }

        public int AddApplicationSettings(ApplicationSettings setting)
        {           
            return _db.GetCollection<ApplicationSettings>(GlobalConstants.AppSettingsCollectionName).Insert(setting);
        }

        public bool UpdateApplicationSettings(ApplicationSettings setting)
        {
            return _db.GetCollection<ApplicationSettings>(GlobalConstants.AppSettingsCollectionName).Update(setting);
        }

        /// <summary>
        /// Checks to see if document exists in collection.  Returns true if it does, false if not.
        /// </summary>
        /// <param name="setting"></param>
        /// <returns><see cref="bool"/></returns>
        public bool CheckApplicationSettingsExists(SettingsNameEnum setting)
        {
            return GetSpecificApplicationSetting(setting) != null ? true : false;
        }
    }
}
