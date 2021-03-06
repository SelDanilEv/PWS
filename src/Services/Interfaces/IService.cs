using Infrastructure.Model.MongoFluentOptions;
using Infrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IService<T> where T:IBaseModel
    {
        Task<IList<T>> GetItems(MongoFluentOptions<T> options);

        Task<T> GetItemById(string id);

        Task AddItem(T newItem);

        Task UpdateItem(T newItem);

        Task RemoveItem(string id);
    }
}
