using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    [OracleCustomTypeMapping("HR.EMPLOYEE")]
    public class EmployeeFactory : IOracleCustomTypeFactory
    {
        public IOracleCustomType CreateObject()
        {
            return new Employee();
        }
    }

    public class Employee : IOracleCustomType, INullable
    {
        [OracleObjectMapping("ID")]
        public int Id { get; set; }

        [OracleObjectMapping("FULL_NAME")]
        public string FullName { get; set; }

        public void FromCustomObject(OracleConnection con, IntPtr pUdt)
        {
            OracleUdt.SetValue(con, pUdt, "ID", Id);
            if (!string.IsNullOrEmpty(FullName))
                OracleUdt.SetValue(con, pUdt, "FULL_NAME", FullName);
        }

        public void ToCustomObject(OracleConnection con, IntPtr pUdt)
        {
            Id = (int)OracleUdt.GetValue(con, pUdt, "ID");
            FullName = (string)OracleUdt.GetValue(con, pUdt, "FULL_NAME");
        }

        public bool IsNull { get; set; }

        public static Employee Null => new Employee { IsNull = true };
    }
}
