using MongoDB.Driver;
using STA.Core.MongoDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace STA.Data.Contract
{
    public class DefaultMongoManager<T> : IDefaultMongoManager<T> where T : BaseBson, new()
    {
        private readonly IMongoCollection<T> Collection;

        public DefaultMongoManager(IMongoDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Collection = database.GetCollection<T>(settings.CollectionName);
        }

        public List<T> Get()
        {
            return Collection.Find(book => true).ToList();
        }

        public T Get(string id)
        {
            return Collection.Find<T>(s => s.Id == id).FirstOrDefault();
        }

        public T Create(T rec)
        {
            Collection.InsertOne(rec);
            return rec;
        }

        public void Update(string id, T recIn)
        {
            Collection.ReplaceOne(s => s.Id == id, recIn);
        }

        public void Remove(T recIn)
        {
            Collection.DeleteOne(s => s.Id == recIn.Id);
        }

        public void Remove(string id)
        {
            Collection.DeleteOne(s => s.Id == id);
        }
    }
}
