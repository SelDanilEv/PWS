using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IRepository<Model>
    {
        Task<IList<Model>> GetItemsAsync();
        Task<IList<Model>> GetItemsWithFilterAsync(FilterDefinition<Model> filter);
        Task<Model> GetItemWithFilterAsync(FilterDefinition<Model> filter);
        Task<Model> GetItemAsync(string id);
        Task AddItemAsync(Model newMenuItem);
        Task UpdateItemAsync(Model newMenuItem);
        Task RemoveItemAsync(string id);
    }
}
