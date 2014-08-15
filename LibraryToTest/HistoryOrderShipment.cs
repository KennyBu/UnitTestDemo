using System;

namespace LibraryToTest
{
    public class HistoryOrderShipment
    {
        public string TrackingNumber { get; set; }
        public string Carrier { get; set; }
        public string Service { get; set; }
        public DateTime DateShipped { get; set; }
    }
}