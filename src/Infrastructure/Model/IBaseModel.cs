using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.Models
{
    public interface IBaseModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        string Id { get; set; }
    }
}
