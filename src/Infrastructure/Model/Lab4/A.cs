

namespace Lab4
{
    public class A
    {
        public string s;
        public int k;
        public float f;

        public A() { }
        public A(string s, int k, float f)
        {
            this.s = s;
            this.k = k;
            this.f = f;
        }
        public static A operator +(A b, A c)
        {
            A result = new A();
            result.s = b.s + c.s;
            result.k = b.k + c.k;
            result.f = b.f + c.f;
            return result;
        }
    }
}