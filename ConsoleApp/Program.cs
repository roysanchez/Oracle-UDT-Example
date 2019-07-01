using Oracle.DataAccess.Client;
using System;
using System.Data;

namespace ConsoleApp
{
    class Program
    {
        static OracleConnection con;

        static void Main(string[] args)
        {
            Console.WriteLine("Start!");
            var conString = "Data Source=localhost:49161/XE; Pooling=false;User id=serv_test; password=serv_test;";

            con = new OracleConnection(conString);
            con.Open();

            var result = GetOrders();
            Console.WriteLine($"Order count: {result.Array.Length}");
            foreach(var order in result.Array)
            {
                Console.WriteLine($"order {order.Id}: {order.Name} - {order.Quantity}");
            }

            con.Close();
            Console.WriteLine("Ended!");
            Console.ReadLine();
        }

        static OrderItemList GetOrders()
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SERV_TEST.TESTFUNCTION";

            //The return values must always be declared as a the first parameter
            var ret = cmd.CreateParameter();
            ret.Direction = ParameterDirection.ReturnValue;
            ret.OracleDbType = OracleDbType.Object;
            ret.UdtTypeName = "SERV_TEST.ORDER_ITEM_LIST_TYP";
            cmd.Parameters.Add(ret);

            cmd.ExecuteNonQuery();

            return ret.Value as OrderItemList;
        }
    }
}