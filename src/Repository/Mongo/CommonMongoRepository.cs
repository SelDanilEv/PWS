using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using Infrastructure.Option;
using Infrastructure.Enum;

namespace Repository.Mongo
{
    public class CommonMongoRepository<T> : BaseMongoRepository<T> where T : IBaseModel, new()
    {
        protected IMongoCollection<T> _mongoCollection;

        public CommonMongoRepository(MongoOptions mongoOption, MongoCollection collection) : base(mongoOption)
        {
            _mongoCollection = database.GetCollection<T>(mongoOption[collection]);
        }

        public override async Task<IList<T>> GetItemsAsync()
        {
            var result = new List<T>();

            try
            {
                var unsortedResult = await _mongoCollection.Find(new BsonDocument()).ToListAsync();
                result = unsortedResult;
            }
            catch (Exception e)
            {
            }

            return result;
        }

        public override async Task<IList<T>> GetItemsWithFilterAsync(FilterDefinition<T> filter)
        {
            var result = new List<T>();

            try
            {
                var unsortedResult = await _mongoCollection.Find(filter).ToListAsync();
                result = unsortedResult;
            }
            catch (Exception e)
            {
            }

            return result;
        }

        public override async Task<T> GetItemWithFilterAsync(FilterDefinition<T> filter)
        {
            var result = new T();

            try
            {
                result = await _mongoCollection.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
            }

            return result;
        }

        public override async Task<T> GetItemAsync(string id)
        {
            var result = new T();

            try
            {
                result = await _mongoCollection.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
            }

            return result;
        }

        public override async Task AddItemAsync(T newUserInfo)
        {
            try
            {
                await _mongoCollection.InsertOneAsync(newUserInfo);
            }
            catch (Exception e)
            {
            }
        }

        public override async Task UpdateItemAsync(T updatedUserInfo)
        {
            try
            {
                await _mongoCollection.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(updatedUserInfo.Id)), updatedUserInfo);
            }
            catch (Exception e)
            {
            }
        }

        public override async Task RemoveItemAsync(string id)
        {
            try
            {
                await _mongoCollection.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
            }
            catch (Exception e)
            {
            }
        }
    }
}
