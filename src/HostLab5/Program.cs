using System;
using System.ServiceModel;
using WCFServiceLab5;

namespace Host2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ServiceHost host = new ServiceHost(typeof(WCFSiplex));
                host.Open();
                Console.WriteLine("Service Hosted Sucessfully");
                Console.Read();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadKey();
            }
        }
    }
}
