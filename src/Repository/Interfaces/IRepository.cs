using Infrastructure.Model.MongoFluentOptions;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IRepository<Model>
    {
        Task<IList<Model>> GetItemsAsync(MongoFluentOptions<Model> options);
        Task<Model> GetItemAsync(string id);
        Task AddItemAsync(Model newMenuItem);
        Task UpdateItemAsync(Model newMenuItem);
        Task RemoveItemAsync(string id);
    }
}
