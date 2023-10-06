using GameServerManager.Models.Options;
using LiteDB;
using Microsoft.Extensions.Options;

namespace GameServerManager.Data
{
    public class LiteDbContext
    {
        public LiteDatabase Database { get; }

        public LiteDbContext(IOptions<LiteDbOptions> options)
        {
            Database = new LiteDatabase(options.Value.DbLocation);
        }

    }
}
