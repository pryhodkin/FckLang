using FckLang.Bot.Models;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace FckLang.Bot.Storage
{
    public class DbInitializer
    {
        public async Task Initialize(string connection, string db, string mapperCollection)
        {
            var settings = MongoClientSettings.FromConnectionString(connection);
            var client = new MongoClient(settings);

            var dbExists = (await (await client.ListDatabaseNamesAsync())
                .ToListAsync())
                .Contains(db);

            if (dbExists)
                return;

            var database = client.GetDatabase(db);

            database.GetCollection<Mapper>(mapperCollection);
        }
    }
}
