using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Model.MongoFluentOptions
{
    public class MongoFluentOptions<T>
    {
        public class Option
        {
            public string Name;
            public string Value;

            public Option(string name,string value)
            {
                Name = name;
                Value = value;
            }

            public Option() { }

            public int GetIntValue()
            {
                int result;
                int.TryParse(this.Value, out result);
                return result;
            }
        }

        public List<Option> options = new List<Option>();

        public FilterDefinition<T> filter= FilterDefinition<T>.Empty;

        public MongoFluentOptions<T> AddOption(Option option)
        {
            options.Add(option);
            return this;
        }
        public MongoFluentOptions<T> RemoveOption(Option option)
        {
            options.Remove(option);
            return this;
        }
        public MongoFluentOptions<T> RemoveAllOptions(Option option)
        {
            options.RemoveAll(x => x.Name == option.Name);
            return this;
        }

        public IEnumerator<Option> GetEnumerator()
        {
            for (int i = 0; i < options.Count; i++)
            {
                yield return options[i];
            }
        }
    }
}
