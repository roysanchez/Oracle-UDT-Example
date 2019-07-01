using ServForOracle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    [UDTName("serv_test.order_item_typ")]
    [UDTCollectionName("serv_test.order_item_list_typ")]
    public class OrderItem
    {
        [UDTProperty("order_id")]
        public long Id { get; set; }

        [UDTProperty("order_name")]
        public string Name { get; set; }

        public int Quantity { get; set; }
    }
}
