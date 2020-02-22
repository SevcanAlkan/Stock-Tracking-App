using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using NGA.Core.MongoDB;
using NGA.Data.Helper;
using NGA.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace NGA.Data.MongoDB
{
    public interface IMongoService
    {
        List<Picture> Get();
        Picture Get(string id);
        Picture Create(Picture picture);
        void Update(string id, Picture pictureIn);
        void Remove(Picture pictureIn);
        void Remove(string id);
    }

    public class MongoService: IMongoService
    {
        private readonly IMongoCollection<Picture> pictures;

        public MongoService()
        {
            IConfiguration config = ConfigrationHelper.Get();

            var client = new MongoClient(config.GetValue<string>("MongoDatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(config.GetValue<string>("MongoDatabaseSettings:DatabaseName"));

            pictures = database.GetCollection<Picture>(config.GetValue<string>("MongoDatabaseSettings:PictureCollectionName"));
        }

        public List<Picture> Get() =>
            pictures.Find(book => true).ToList();

        public Picture Get(string id) =>
            pictures.Find<Picture>(s => s.Id == id).FirstOrDefault();

        public Picture Create(Picture picture)
        {
            pictures.InsertOne(picture);
            return picture;
        }

        public void Update(string id, Picture pictureIn) =>
            pictures.ReplaceOne(s => s.Id == id, pictureIn);

        public void Remove(Picture pictureIn) =>
            pictures.DeleteOne(s => s.Id == pictureIn.Id);

        public void Remove(string id) =>
            pictures.DeleteOne(s => s.Id == id);
    }
}
