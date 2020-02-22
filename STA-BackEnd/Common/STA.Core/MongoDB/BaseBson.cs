using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace STA.Core.MongoDB
{
    public class BaseBson
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
