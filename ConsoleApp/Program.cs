using ServForOracle;
using System;
using System.Data;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start!");
            var serv = new DefaultServiceForOracle("Data Source=IP:PORT/ORCL; Pooling=True;User id=serv_test; password=serv_test;");

            var parameter = Param.Input(new OrderItem { Id = 1 });

            var orderList = serv.ExecuteFunction<OrderItem[]>("serv_test.TestFunction", parameter);

            foreach (var order in orderList)
            {
                Console.WriteLine($"order {order.Id}: {order.Name} - {order.Quantity}");
            }
            Console.WriteLine("Ended!");
            Console.ReadLine();
        }
    }
}