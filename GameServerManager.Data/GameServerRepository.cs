using GameServerManager.Models;
using GameServerManager.Models.Enums;
using GameServerManager.Models.Servers;
using LiteDB;

namespace GameServerManager.Data
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

        public int AddGameServer(GameServer server, ServerTypeEnum type)
        {
            switch (type)
            {
                case (ServerTypeEnum)0:
                    return _db.GetCollection<GameServer>(GlobalConstants.GameServerCollectionName).Insert((GBServer)server);
                case (ServerTypeEnum)1:
                    return 0;
                case (ServerTypeEnum)2:
                    return 0;
                default:
                    return 0;                    
            }

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
