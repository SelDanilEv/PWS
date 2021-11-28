using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using Lab7_service;

namespace Lab7_host
{
    class Program
    {
        static void Main(string[] args)
        {
            var baseAddress = new Uri("http://localhost:8008/FeedService");
            var svcHost = new WebServiceHost(typeof(FeedService), baseAddress);
            //ServiceHost host = new ServiceHost(typeof(IFeedService));
            svcHost.Open();
            Console.WriteLine("Host Open");
            Console.ReadLine();
        }
    }
}
