using System;

namespace LibraryToTest
{
    public interface ICustomerManager
    {
        Customer GetCustomer(Guid id);
    }
}