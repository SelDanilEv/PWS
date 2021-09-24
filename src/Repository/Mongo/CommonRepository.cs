using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using Infrastructure.Option;
using Infrastructure.Enum;
using Repository.Interfaces;
using Infrastructure.Model.MongoFluentOptions;

namespace Repository.Mongo
{
    public class CommonRepository<T> : IRepository<T> where T : IBaseModel
    {
        protected CommonMongoRepository<T> repository;

        public CommonRepository(MongoOptions mongoOption, MongoCollection collection)
        {
            this.repository = new CommonMongoRepository<T>(mongoOption, collection);
        }

        public async Task<T> GetItemAsync(string id)
        {
            return await repository.GetItemAsync(id).FirstOrDefaultAsync();
        }

        public async Task<IList<T>> GetItemsAsync(MongoFluentOptions<T> options)
        {
            if (options == null) options = new MongoFluentOptions<T>();

            return await repository.GetItemsAsync(options).ToListAsync();
        }

        public async Task AddItemAsync(T newItem)
        {
            if (newItem != null)
            {
                repository.AddItemAsync(newItem);
            }
        }

        public async Task RemoveItemAsync(string id)
        {
            repository.RemoveItemAsync(id);
        }

        public async Task UpdateItemAsync(T newMenuItem)
        {
            if (newMenuItem != null)
            {
                repository.UpdateItemAsync(newMenuItem);
            }
        }
    }
}
