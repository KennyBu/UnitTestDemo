using System;
using System.Collections.Generic;
using LibraryToTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTestProject
{
    [TestClass]
    public class ReturnsManagerTests
    {
        private Guid _orderId;
        private Guid _customerId;
        private Mock<IOrderManagerQuery> _orderManagerQuery;
        private Mock<ICustomerManager> _customerManager;
        private IReturnsManager _manager;

        [TestInitialize]
        public void Startup()
        {
            _customerId = Guid.NewGuid();
            _orderId = Guid.NewGuid();
            _orderManagerQuery = new Mock<IOrderManagerQuery>();
            _customerManager = new Mock<ICustomerManager>();
        }

        [TestMethod]
        public void OrderHasReturnDocumentSuccess()
        {
            _orderManagerQuery.Setup(s => s.GetOrder(_orderId)).Returns(new Order { OrderId = _orderId, CustomerId = _customerId });
            _customerManager.Setup(s => s.GetCustomer(_customerId)).Returns(new Customer { ShippingCountry = "US United States" });
            _manager = new ReturnsManager(_orderManagerQuery.Object, _customerManager.Object);

            var historyOrder = new HistoryOrder
            {
                OrderID = _orderId,
                OrderStatus = "Shipped",
                Shipments = new List<HistoryOrderShipment>
                        {
                            new HistoryOrderShipment {DateShipped = DateTime.Now.AddDays(-1)}
                        }
            };

            var flag = _manager.OrderHasReturnDocument(historyOrder);

            Assert.IsTrue(flag);
        }

        [TestMethod]
        public void OrderHasReturnDocumentSuccessOrderOldButGood()
        {
            _orderManagerQuery.Setup(s => s.GetOrder(_orderId)).Returns(new Order { OrderId = _orderId, CustomerId = _customerId });
            _customerManager.Setup(s => s.GetCustomer(_customerId)).Returns(new Customer { ShippingCountry = "US United States" });
            _manager = new ReturnsManager(_orderManagerQuery.Object, _customerManager.Object);

            var historyOrder = new HistoryOrder
            {
                OrderID = _orderId,
                OrderStatus = "Shipped",
                Shipments = new List<HistoryOrderShipment>
                        {
                            new HistoryOrderShipment {DateShipped = DateTime.Now.AddDays(-45)}
                        }
            };

            var flag = _manager.OrderHasReturnDocument(historyOrder);

            Assert.IsTrue(flag);
        }

        [TestMethod]
        public void OrderHasReturnDocumentFailNotShipped()
        {
            _orderManagerQuery.Setup(s => s.GetOrder(_orderId)).Returns(new Order { OrderId = _orderId, CustomerId = _customerId });
            _customerManager.Setup(s => s.GetCustomer(_customerId)).Returns(new Customer { ShippingCountry = "US United States" });
            _manager = new ReturnsManager(_orderManagerQuery.Object, _customerManager.Object);

            var historyOrder = new HistoryOrder
            {
                OrderID = _orderId,
                OrderStatus = "Processing",
                Shipments = new List<HistoryOrderShipment>
                        {
                            new HistoryOrderShipment {DateShipped = DateTime.Now.AddDays(-1)}
                        }
            };

            var flag = _manager.OrderHasReturnDocument(historyOrder);

            Assert.IsFalse(flag);
        }

        [TestMethod]
        public void OrderHasReturnDocumentFailNoShipments()
        {
            _orderManagerQuery.Setup(s => s.GetOrder(_orderId)).Returns(new Order { OrderId = _orderId, CustomerId = _customerId });
            _customerManager.Setup(s => s.GetCustomer(_customerId)).Returns(new Customer { ShippingCountry = "US United States" });
            _manager = new ReturnsManager(_orderManagerQuery.Object, _customerManager.Object);

            var historyOrder = new HistoryOrder
            {
                OrderID = _orderId,
                OrderStatus = "Shipped"
            };

            var flag = _manager.OrderHasReturnDocument(historyOrder);

            Assert.IsFalse(flag);
        }

        [TestMethod]
        public void OrderHasReturnDocumentFailWholesale()
        {
            _orderManagerQuery.Setup(s => s.GetOrder(_orderId)).Returns(new Order { OrderId = _orderId, CustomerId = _customerId });
            _customerManager.Setup(s => s.GetCustomer(_customerId)).Returns(new Customer { ShippingCountry = "US United States" });
            _manager = new ReturnsManager(_orderManagerQuery.Object, _customerManager.Object);

            var historyOrder = new HistoryOrder
            {
                OrderID = _orderId,
                OrderStatus = "Shipped",
                Wholesale = true,
                Shipments = new List<HistoryOrderShipment>
                        {
                            new HistoryOrderShipment {DateShipped = DateTime.Now.AddDays(-1)}
                        }
            };

            var flag = _manager.OrderHasReturnDocument(historyOrder);

            Assert.IsFalse(flag);
        }

        [TestMethod]
        public void OrderHasReturnDocumentFailDropship()
        {
            _orderManagerQuery.Setup(s => s.GetOrder(_orderId)).Returns(new Order { OrderId = _orderId, CustomerId = _customerId });
            _customerManager.Setup(s => s.GetCustomer(_customerId)).Returns(new Customer { ShippingCountry = "US United States" });
            _manager = new ReturnsManager(_orderManagerQuery.Object, _customerManager.Object);

            var historyOrder = new HistoryOrder
            {
                OrderID = _orderId,
                OrderStatus = "Shipped",
                Dropship = true,
                Shipments = new List<HistoryOrderShipment>
                        {
                            new HistoryOrderShipment {DateShipped = DateTime.Now.AddDays(-1)}
                        }
            };

            var flag = _manager.OrderHasReturnDocument(historyOrder);

            Assert.IsFalse(flag);
        }

        [TestMethod]
        public void OrderHasReturnDocumentFailNotDomestic()
        {
            _orderManagerQuery.Setup(s => s.GetOrder(_orderId)).Returns(new Order { OrderId = _orderId, CustomerId = _customerId });
            _customerManager.Setup(s => s.GetCustomer(_customerId)).Returns(new Customer { ShippingCountry = "FR France" });
            _manager = new ReturnsManager(_orderManagerQuery.Object, _customerManager.Object);

            var historyOrder = new HistoryOrder
            {
                OrderID = _orderId,
                OrderStatus = "Shipped",
                Shipments = new List<HistoryOrderShipment>
                        {
                            new HistoryOrderShipment {DateShipped = DateTime.Now.AddDays(-1)}
                        }
            };

            var flag = _manager.OrderHasReturnDocument(historyOrder);

            Assert.IsFalse(flag);
        }

        [TestMethod]
        public void OrderHasReturnDocumentFailOrderTooOld()
        {
            _orderManagerQuery.Setup(s => s.GetOrder(_orderId)).Returns(new Order { OrderId = _orderId, CustomerId = _customerId });
            _customerManager.Setup(s => s.GetCustomer(_customerId)).Returns(new Customer { ShippingCountry = "US United States" });
            _manager = new ReturnsManager(_orderManagerQuery.Object, _customerManager.Object);

            var historyOrder = new HistoryOrder
            {
                OrderID = _orderId,
                OrderStatus = "Shipped",
                Shipments = new List<HistoryOrderShipment>
                        {
                            new HistoryOrderShipment {DateShipped = DateTime.Now.AddDays(-46)}
                        }
            };

            var flag = _manager.OrderHasReturnDocument(historyOrder);

            Assert.IsFalse(flag);
        }

        [TestMethod]
        public void OrderHasReturnDocumentFailOrderReallyTooOld()
        {
            _orderManagerQuery.Setup(s => s.GetOrder(_orderId)).Returns(new Order { OrderId = _orderId, CustomerId = _customerId });
            _customerManager.Setup(s => s.GetCustomer(_customerId)).Returns(new Customer { ShippingCountry = "US United States" });
            _manager = new ReturnsManager(_orderManagerQuery.Object, _customerManager.Object);

            var historyOrder = new HistoryOrder
            {
                OrderID = _orderId,
                OrderStatus = "Shipped",
                Shipments = new List<HistoryOrderShipment>
                        {
                            new HistoryOrderShipment {DateShipped = DateTime.Now.AddMonths(-3)}
                        }
            };

            var flag = _manager.OrderHasReturnDocument(historyOrder);

            Assert.IsFalse(flag);
        }
    }
}