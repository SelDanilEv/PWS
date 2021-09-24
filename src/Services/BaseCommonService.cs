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
    public abstract class BaseCommonService<T> : IService<T> where T : IBaseModel
    {
        protected IRepository<T> _repository;

        public BaseCommonService(MongoOptions mongoOptions)
        {
            _repository = RepositoryFactory.CreateRepository<T>(mongoOptions);
        }

        public virtual Task AddItem(T newItem)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<T> GetItemById(string id)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<IList<T>> GetItems(MongoFluentOptions<T> options)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task RemoveItem(string id)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task UpdateItem(T newItem)
        {
            throw new System.NotImplementedException();
        }
    }
}
