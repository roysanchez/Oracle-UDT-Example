using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    [OracleCustomTypeMapping("SERV_TEST.ORDER_ITEM_TYP")]
    public class OrderItemFactory : IOracleCustomTypeFactory
    {
        public IOracleCustomType CreateObject()
        {
            return new OrderItem();
        }
    }

    public class OrderItem : IOracleCustomType, INullable
    {
        [OracleObjectMapping("ORDER_ID")]
        public int Id { get; set; }

        [OracleObjectMapping("ORDER_NAME")]
        public string Name { get; set; }

        [OracleObjectMapping("QUANTITY")]
        public int Quantity { get; set; }

        public void FromCustomObject(OracleConnection con, IntPtr pUdt)
        {
            OracleUdt.SetValue(con, pUdt, "ID", Id);
            OracleUdt.SetValue(con, pUdt, "QUANTITY", Quantity);
            if (!string.IsNullOrEmpty(Name))
                OracleUdt.SetValue(con, pUdt, "ORDER_NAME", Name);
        }

        public void ToCustomObject(OracleConnection con, IntPtr pUdt)
        {
            Id = (int)OracleUdt.GetValue(con, pUdt, "ID");
            Quantity = (int)OracleUdt.GetValue(con, pUdt, "QUANTITY");
            Name = (string)OracleUdt.GetValue(con, pUdt, "ORDER_NAME");
        }

        public bool IsNull { get; set; }

        public static OrderItem Null => new OrderItem { IsNull = true };
    }
}
