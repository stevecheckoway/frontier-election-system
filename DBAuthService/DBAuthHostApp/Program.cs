using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

using Sequoia.DBAuthService;

namespace DBAuthHostApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** DBAuth Service Host *****");
            using (var serviceHost = new AuthorizationCustomHost(typeof(DBAuthService)))
            {
                serviceHost.Open();

                Console.WriteLine("The service is ready.");

                Console.WriteLine("Press the Enter key to terminate the service.");
            }
        }
    }
}
