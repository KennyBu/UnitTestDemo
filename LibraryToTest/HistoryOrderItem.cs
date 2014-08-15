using System;

namespace LibraryToTest
{
    public class HistoryOrderItem
    {
        public Guid OrderID { get; set; }
        public Guid OrderDetailId { get; set; }

        public string ProductCode { get; set; }
        public string NameShort { get; set; }
        public string Desc { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public string Size { get; set; }
        public string Status { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int QtyShipped { get; set; }
        public bool HasSkuImage { get; set; }

        public int Availability { get; set; }
        public int QtyAvailableInWarehouse { get; set; }
        public string PcomBrandId { get; set; }
        public string PcomProductId { get; set; }
    }
}