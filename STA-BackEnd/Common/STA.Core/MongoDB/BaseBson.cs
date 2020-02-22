using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace STA.Core.MongoDB
{
    public class BaseBson : IBaseBson
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }

    public interface IBaseBson
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        string Id { get; set; }
    }
}
