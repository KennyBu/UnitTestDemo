using System;
using System.Linq;

namespace LibraryToTest
{
    public class ReturnsManager : IReturnsManager
    {
        private readonly IOrderManagerQuery _orderManagerQuery;
        private readonly ICustomerManager _customerManager;

        public ReturnsManager(IOrderManagerQuery orderManagerQuery, ICustomerManager customerManager)
        {
            _orderManagerQuery = orderManagerQuery;
            _customerManager = customerManager;
        }

        /// <summary>
        /// This method checks to see if an order in the history should have a Return Document.
        /// Business Rules Are: order has shipped within USA and shipped within 45 days
        /// </summary>
        /// <param name="historyOrder"></param>
        /// <returns></returns>
        public bool OrderHasReturnDocument(HistoryOrder historyOrder)
        {
            var hasReturnPdf = false;

            if (!historyOrder.Wholesale && !historyOrder.Dropship)
            {
                var shipped = (historyOrder.OrderStatus.ToLower() == "shipped") && historyOrder.Shipments != null &&
                              historyOrder.Shipments.Count > 0;
                if (shipped)
                {
                    var shipment = historyOrder.Shipments.FirstOrDefault();
                    if (shipment != null)
                    {
                        var daysSinceShipped = (DateTime.Now - shipment.DateShipped).Days;
                        if (daysSinceShipped <= 45)
                        {
                            var order = _orderManagerQuery.GetOrder(historyOrder.OrderID);
                            if (order != null)
                            {
                                var customer = _customerManager.GetCustomer(order.CustomerId);
                                if (customer != null)
                                {
                                    var shippingCountry = customer.ShippingCountry.Substring(0, 2);
                                    //if (shippingCountry == "US")
                                    if (shippingCountry == CountryType.UnitedStates.ToString())
                                    {
                                        hasReturnPdf = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return hasReturnPdf;
        }
    }
}