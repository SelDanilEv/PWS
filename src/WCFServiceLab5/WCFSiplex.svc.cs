using Infrastructure.Model.Lab4;
using System;

namespace WCFServiceLab5
{
    public class WCFSiplex : IWCFSiplex
    {
        public int Add(int x, int y)
        {
            return x + y;
        }

        public string Concat(string x, double y)
        {
            return $"{x}{y.ToString()}";
        }

        public A Sum(A x, A y)
        {
            return x + y;
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
