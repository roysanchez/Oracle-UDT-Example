using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using ServForOracle.NetCore;
using ServForOracle.NetCore.Extensions;
using ServForOracle.NetCore.Parameters;
using System;
using System.Data;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start!");
            var conString = "Data Source=localhost:49161/XE; Pooling=false;User id=serv_test; password=serv_test;";
            var factory = new LoggerFactory();

            var service = new ServiceForOracle(factory.CreateLogger<ServiceForOracle>(), new MemoryCache(new MemoryCacheOptions()), conString);

            var orders = service.ExecuteFunction<OrderItem[], OrderItem>("serv_test.TestFunction", null);
            //It's the same as 
            //var orders = service.ExecuteFunction<OrderItem[]>("serv_test.TestFunction", Param.Input<OrderItem>(null));
            
            foreach(var order in orders)
            {
                Console.WriteLine($"order {order.Id}: {order.Name} - {order.Quantity}");
            }


            Console.WriteLine("Ended!");
            Console.ReadLine();
        }
    }
}