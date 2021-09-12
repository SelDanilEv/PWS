using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using MongoDB.Driver;
using Infrastructure.Option;
using Infrastructure.Models;
using Repository.Interfaces;

namespace Repository.Mongo
{
    public abstract class BaseMongoRepository<Model> : IRepository<Model> where Model : IBaseModel
    {
        protected IMongoDatabase database;

        public BaseMongoRepository(MongoOptions mongoOption)
        {
            var connection = new MongoUrlBuilder(mongoOption.ConnectionString);
            MongoClient client = new MongoClient(mongoOption.ConnectionString);

            database = client.GetDatabase(connection.DatabaseName);
        }

        public virtual Task AddItemAsync(Model newMenuItem)
        {
            throw new NotImplementedException();
        }

        public virtual Task<Model> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IList<Model>> GetItemsAsync()
        {
            throw new NotImplementedException();
        }

        public virtual Task<IList<Model>> GetItemsWithFilterAsync(FilterDefinition<Model> filter)
        {
            throw new NotImplementedException();
        }

        public virtual Task<Model> GetItemWithFilterAsync(FilterDefinition<Model> filter)
        {
            throw new NotImplementedException();
        }

        public virtual Task RemoveItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public virtual Task UpdateItemAsync(Model newMenuItem)
        {
            throw new NotImplementedException();
        }

        protected IList<Model> SortById(IList<Model> unsortedList)
        {
            var sortedList = unsortedList.OrderBy(x => x.Id);
            return sortedList.ToList();
        }
    }

    public class BaseJSONRepository : BaseMongoRepository<BaseModel>
    {
        public BaseJSONRepository(MongoOptions mongoOption) : base (mongoOption)
        {
        }
    }
}
