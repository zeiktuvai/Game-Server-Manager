using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBServerManager2.Data
{
    public class GameServerRepository
    {
        private LiteDatabase _db;

        public GameServerRepository(LiteDbContext dbContext)
        {
            _db = dbContext.Database;
        }


    }
}
