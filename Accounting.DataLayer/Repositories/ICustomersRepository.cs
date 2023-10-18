using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.DataLayer.Repositories
{
    public interface ICustomersRepository
    {
        List<Customers> getAllCustomers();
        Customers getCustomerById(int customerId);
        bool insertCustomer(Customers customer);
        bool updateCustomer(Customers customer);
        bool deleteCustomer(Customers customer);
        bool deleteCustomer(int customerId);
        void save();
    }
}
