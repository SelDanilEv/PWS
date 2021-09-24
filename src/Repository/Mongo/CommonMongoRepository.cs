using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using Infrastructure.Option;
using Infrastructure.Enum;
using Infrastructure.Model.MongoFluentOptions;
using Repository.Helpers;

namespace Repository.Mongo
{
    public class CommonMongoRepository<T> : BaseMongoRepository<T> where T : IBaseModel
    {
        protected IMongoCollection<T> _mongoCollection;

        public CommonMongoRepository(MongoOptions mongoOption, MongoCollection collection) : base(mongoOption)
        {
            _mongoCollection = database.GetCollection<T>(mongoOption[collection]);
        }

        public override IFindFluent<T, T> GetItemsAsync(MongoFluentOptions<T> options)
        {
            try
            {
                var fluentHelper = new FindFluentHelper();
                var commonResult = _mongoCollection.Find(options.filter);
                var result = fluentHelper.ApplyFluentOptions(commonResult, options);
                return result;
            }
            catch (Exception e)
            {
            }

            return null;
        }

        public override IFindFluent<T, T> GetItemAsync(string id)
        {
            try
            {
                return _mongoCollection.Find(new BsonDocument("_id", new ObjectId(id)));
            }
            catch (Exception e)
            {
            }

            return null;
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
