using MongoDB.Driver;
using System.Collections;
using System.Collections.Generic;

namespace FckLang.Bot.Storage
{
    public class MongoStorage<T> : IStorage<T>
    {
        private MongoClientSettings connection_;
        private string db_;
        private string collection_;

        public MongoStorage(MongoClientSettings connection, string db, string collection)
        {
            connection_ = connection;
            db_ = db;
            collection_ = collection;
        }


        public IEnumerator<T> GetEnumerator()
        {
            return new MongoClient(connection_)
                .GetDatabase(db_)
                .GetCollection<T>(collection_)
                .AsQueryable()
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
