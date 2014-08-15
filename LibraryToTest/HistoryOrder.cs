using System;
using System.Collections.Generic;

namespace LibraryToTest
{
    public class HistoryOrder
    {
        public Guid OrderID { get; set; }

        public string OrderIdInt { get; set; }
        public string InternalOrderid { get; set; }
        public DateTime DateCreated { get; set; }

        public string OrderStatus { get; set; }

        public List<HistoryOrderShipment> Shipments { get; set; }

        public decimal GrandTotal { get; set; }

        public bool Wholesale { get; set; }
        public bool Dropship { get; set; }

        public bool HasItemUpdate { get; set; }

        public List<HistoryOrderItem> Items { get; set; } 
    }
}