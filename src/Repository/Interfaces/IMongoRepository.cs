using Infrastructure.Model.MongoFluentOptions;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IMongoRepository<Model>
    {
        IFindFluent<Model,Model> GetItemsAsync(MongoFluentOptions<Model> options);
        IFindFluent<Model, Model> GetItemAsync(string id);
        Task AddItemAsync(Model newItem);
        Task UpdateItemAsync(Model newItem);
        Task RemoveItemAsync(string id);
    }
}
