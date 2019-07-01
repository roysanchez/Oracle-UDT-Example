using ServForOracle.NetCore.OracleAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    [OracleUdt("serv_test", "order_item_typ", "order_item_list_typ")]
    public class OrderItem
    {
        [OracleUdtProperty("order_id")]
        public long Id { get; set; }

        [OracleUdtProperty("order_name")]
        public string Name { get; set; }

        public int Quantity { get; set; }
    }
}
