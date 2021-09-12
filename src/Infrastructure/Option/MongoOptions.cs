using Infrastructure.Enum;

namespace Infrastructure.Option
{
    public class MongoOptions
    {
        public const string Name = "MongoOption";
        public const string MongoConnectionString = "MongoConnectionString.txt";

        public string ConnectionString { get; set; }
        public string CollectionPrefix { get; set; }
        public string StudentCollectionName => "student";

        public string this[MongoCollection collection]
        {
            get
            {
                var result = CollectionPrefix;
                switch (collection)
                {
                    case MongoCollection.Student: result += StudentCollectionName; break;
                    default: return null;
                }
                return result;
            }
        }
    }
}
