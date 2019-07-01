using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    [OracleCustomTypeMapping("SERV_TEST.ORDER_ITEM_LIST_TYP")]
    public class OrderItemListFactory : IOracleCustomTypeFactory, IOracleArrayTypeFactory
    {
        public IOracleCustomType CreateObject()
        {
            return new OrderItemList();
        }

        public Array CreateArray(int numElems)
        {
            return new OrderItem[numElems];
        }

        public Array CreateStatusArray(int numElems)
        {
            return new OracleUdtStatus[numElems];
        }
    }

    public class OrderItemList : IOracleCustomType
    {
        [OracleArrayMapping]
        public OrderItem[] Array { get; set; }

        private OracleUdtStatus[] m_statusArray;

        public void FromCustomObject(OracleConnection con, IntPtr pUdt)
        {
            OracleUdt.SetValue(con, pUdt, 0, Array, m_statusArray);
        }

        public void ToCustomObject(OracleConnection con, IntPtr pUdt)
        {
            object objectStatusArray = null;
            Array = (OrderItem[])OracleUdt.GetValue(con, pUdt, 0, out objectStatusArray);
            m_statusArray = (OracleUdtStatus[])objectStatusArray;
        }

        public bool IsNull { get; set; }

        public static OrderItemList Null => new OrderItemList { IsNull = true };
    }
}
