using Accounting.DataLayer;
using Accounting.DataLayer.Repositories;
using Accounting.DataLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAppConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ICustomersRepository customer = new CustomerRepository();
            Customers newCustomer = new Customers()
            {
                FullName = "سبحان ضیائی",
                Mobile = "09158251054",
                CustomerImage = "No photo",
            };
            customer.insertCustomer(newCustomer);
            customer.save();
            var list = customer.getAllCustomers();
        }
    }
}
