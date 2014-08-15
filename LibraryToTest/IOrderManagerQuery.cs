using System;

namespace LibraryToTest
{
    public interface IOrderManagerQuery
    {
        Order GetOrder(Guid orderId);
    }
}