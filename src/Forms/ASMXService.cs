using Infrastructure.Model.Lab4;
using System.Xml.Serialization;

namespace Forms
{
    class ASMXService : ISimplexSoap
    {
        [return: XmlElement("sumResult")]
        public int Add(int x, int y)
        {
            return x + y;
        }

        [return: XmlElement("sumjsonResult")]
        public int Adds(int x, int y)
        {
            return x + y;
        }

        [return: XmlElement("concatResult")]
        public string Concat(string s, double d)
        {
            return $"{s}{d.ToString()}";
        }

        [return: XmlElement("getjsonAResult")]
        public A GetA()
        {
            return new A();
        }

        [return: XmlElement("sumAResult")]
        public A Sum(A a1, A a2)
        {
            return new A()
            {
                s = a1.s + a2.s,
                f = a1.f + a2.f,
                k = a1.k + a2.k
            };
        }
    }
}
