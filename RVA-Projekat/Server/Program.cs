using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Server.ServiceProviders;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {

            ServiceHost svc = new ServiceHost(typeof(UserService));
            svc.Open();


            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }
    }
}
