using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.Models
{
    public abstract class BaseModel:IBaseModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
