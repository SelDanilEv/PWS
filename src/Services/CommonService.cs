using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Interfaces;
using Infrastructure.Models;
using Repository;
using Repository.Interfaces;
using Infrastructure.Option;

namespace Services
{
    public class CommonService<T> : IService<T> where T : IBaseModel
    {
        private IRepository<T> _repository;

        public CommonService(MongoOptions mongoOptions)
        {
            _repository = RepositoryFactory.CreateRepository<T>(mongoOptions);
        }

        #region CRUD
        public async Task<IList<T>> GetItems()
        {
            var itemsResult = await _repository.GetItemsAsync();

            return itemsResult;
        }

        public async Task<T> GetItemById(string id)
        {
            var itemResult = await _repository.GetItemAsync(id);

            return itemResult;
        }

        public async Task AddItem(T newItem)
        {
            await _repository.AddItemAsync(newItem);
        }

        public async Task UpdateItem(T newItem)
        {
            await _repository.UpdateItemAsync(newItem);
        }

        public async Task RemoveItem(string id)
        {
            await _repository.RemoveItemAsync(id);
        }
        #endregion
    }
}
