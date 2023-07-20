using GBServerManager2.Models;
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

        public IEnumerable<GameServer> GetAllGameServers()
        {
            return _db.GetCollection<GameServer>(GlobalConstants.GameServerCollectionName).FindAll();
        }

        public GameServer GetGameServer(int serverId) 
        { 
            return _db.GetCollection<GameServer>(GlobalConstants.GameServerCollectionName).FindOne(
                s => s.id == serverId);
        }

        public int AddGameServer(GameServer server)
        {
            return _db.GetCollection<GameServer>(GlobalConstants.GameServerCollectionName).Insert(server);
        }

        public bool UpdatedGameServer(GameServer server)
        {
            return _db.GetCollection<GameServer>(GlobalConstants.GameServerCollectionName).Update(server);
        }

        public void DeleteGameServer(GameServer server)
        {
            //var srvr = GetGameServer(server.ServerId);
            _db.GetCollection<GameServer>(GlobalConstants.GameServerCollectionName).Delete(server.id);
        }

    }
}
