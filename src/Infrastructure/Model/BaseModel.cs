using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Infrastructure.Models
{
    [Serializable]
    public abstract class BaseModel : IBaseModel
    {
        private string id;

        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get => id; set => id = value; }
    }
}
