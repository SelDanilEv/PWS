using System;
using Infrastructure.Enum;
using Infrastructure.Model.Lab3;
using Infrastructure.Option;

using Repository.Interfaces;
using Repository.Mongo;

namespace Repository
{
    public static class RepositoryFactory
    {
        public static IRepository<T> CreateRepository<T>(MongoOptions mongoOption = null)
        {

            if (typeof(T) == typeof(Student))
            {
                return (IRepository<T>)new CommonRepository<Student>(mongoOption, MongoCollection.Student);
            }

            throw new InvalidOperationException();
        }
    }
}
