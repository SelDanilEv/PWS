using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using MongoDB.Driver;
using Infrastructure.Option;
using Infrastructure.Models;
using Repository.Interfaces;
using Infrastructure.Model.MongoFluentOptions;

namespace Repository.Mongo
{
    public abstract class BaseMongoRepository<Model> : IMongoRepository<Model> where Model : IBaseModel
    {
        protected IMongoDatabase database;

        public BaseMongoRepository(MongoOptions mongoOption)
        {
            var connection = new MongoUrlBuilder(mongoOption.ConnectionString);
            MongoClient client = new MongoClient(mongoOption.ConnectionString);

            database = client.GetDatabase(connection.DatabaseName);
        }

        public virtual Task AddItemAsync(Model newItem)
        {
            throw new NotImplementedException();
        }

        public virtual IFindFluent<Model, Model> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public virtual IFindFluent<Model, Model> GetItemsAsync(MongoFluentOptions<Model> options)
        {
            throw new NotImplementedException();
        }

        public virtual Task RemoveItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public virtual Task UpdateItemAsync(Model newItem)
        {
            throw new NotImplementedException();
        }

        protected IList<Model> SortById(IList<Model> unsortedList)
        {
            var sortedList = unsortedList.OrderBy(x => x.Id);
            return sortedList.ToList();
        }
    }
}
