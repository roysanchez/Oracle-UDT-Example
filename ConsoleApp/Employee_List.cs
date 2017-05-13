using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    [OracleCustomTypeMapping("HR.EMPLOYEE_LIST")]
    public class EmployeeListFactory : IOracleCustomTypeFactory, IOracleArrayTypeFactory
    {
        public IOracleCustomType CreateObject()
        {
            return new EmployeeList();
        }

        public Array CreateArray(int numElems)
        {
            return new Employee[numElems];
        }

        public Array CreateStatusArray(int numElems)
        {
            return new OracleUdtStatus[numElems];
        }
    }

    public class EmployeeList : IOracleCustomType
    {
        [OracleArrayMapping]
        public Employee[] Array { get; set; }

        private OracleUdtStatus[] m_statusArray;

        public void FromCustomObject(OracleConnection con, IntPtr pUdt)
        {
            OracleUdt.SetValue(con, pUdt, 0, Array, m_statusArray);
        }

        public void ToCustomObject(OracleConnection con, IntPtr pUdt)
        {
            object objectStatusArray = null;
            Array = (Employee[])OracleUdt.GetValue(con, pUdt, 0, out objectStatusArray);
            m_statusArray = (OracleUdtStatus[])objectStatusArray;
        }

        public bool IsNull { get; set; }

        public EmployeeList Null => new EmployeeList { IsNull = true };
    }
}
