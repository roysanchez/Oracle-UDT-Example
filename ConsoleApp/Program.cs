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
            var conString = "Data Source=localhost:49161/XE; Pooling=false;User id=HR; password=hr;";

            con = new OracleConnection(conString);
            con.Open();

            var result = Get_Employee(100);
            Console.WriteLine("Employee Id No. {0}: {1}", result.Id, result.FullName);

            var res = Get();
            Console.WriteLine("Employees count: {0}", res.Array.Length);

            con.Close();
            Console.WriteLine("Ended!");
            Console.ReadLine();
        }

        static Employee Get_Employee(int id)
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "HR.EMPLOYEE_UDT_EXAMPLE.GET_EMPLOYEE";

            //The return values must always be declared as a the first parameter
            var ret = cmd.CreateParameter();
            ret.Direction = ParameterDirection.ReturnValue;
            ret.OracleDbType = OracleDbType.Object;
            ret.UdtTypeName = "HR.EMPLOYEE";
            cmd.Parameters.Add(ret);

            OracleParameter param = cmd.CreateParameter();
            param.OracleDbType = OracleDbType.Int32;
            param.Direction = ParameterDirection.Input;
            param.Value = id;
            cmd.Parameters.Add(param);
            
            cmd.ExecuteNonQuery();

            return ret.Value as Employee;
        }

        static EmployeeList Get()
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "HR.EMPLOYEE_UDT_EXAMPLE.GET";

            //The return values must always be declared as a the first parameter
            var ret = cmd.CreateParameter();
            ret.Direction = ParameterDirection.ReturnValue;
            ret.OracleDbType = OracleDbType.Object;
            ret.UdtTypeName = "HR.EMPLOYEE_LIST";
            cmd.Parameters.Add(ret);

            cmd.ExecuteNonQuery();

            return ret.Value as EmployeeList;
        }
    }
}