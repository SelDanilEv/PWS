using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Interfaces;
using Infrastructure.Models;
using Repository;
using Repository.Interfaces;
using Infrastructure.Option;
using Infrastructure.Model.MongoFluentOptions;

namespace Services
{
    public class CommonService<T> : BaseCommonService<T> where T:IBaseModel
    {
        public CommonService(MongoOptions mongoOptions) : base(mongoOptions)
        {
        }

        #region CRUD
        public override async Task<IList<T>> GetItems(MongoFluentOptions<T> options)
        {
            var itemsResult = await _repository.GetItemsAsync(options);

            return itemsResult;
        }

        public override async Task<T> GetItemById(string id)
        {
            var itemResult = await _repository.GetItemAsync(id);

            return itemResult;
        }

        public override async Task AddItem(T newItem)
        {
            await _repository.AddItemAsync(newItem);
        }

        public override async Task UpdateItem(T newItem)
        {
            await _repository.UpdateItemAsync(newItem);
        }

        public override async Task RemoveItem(string id)
        {
            await _repository.RemoveItemAsync(id);
        }
        #endregion
    }
}
