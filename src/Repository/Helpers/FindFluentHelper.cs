using Infrastructure.Model.MongoFluentOptions;
using MongoDB.Driver;

namespace Repository.Helpers
{
    public class FindFluentHelper
    {
        public IFindFluent<T, T> ApplyFluentOptions<T>(IFindFluent<T, T> fluentResult, MongoFluentOptions<T> options)
        {
            foreach (var option in options)
            {
                switch (option.Name.ToLower())
                {
                    case "limit":
                        fluentResult = fluentResult.Limit(option.GetIntValue());
                        break;
                    case "skip":
                        fluentResult = fluentResult.Skip(option.GetIntValue());
                        break;
                    case "sort":
                        fluentResult = fluentResult.Sort(option.Value);
                        break;
                }
            }

            return fluentResult;
        }
    }
}
