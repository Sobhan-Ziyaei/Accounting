﻿using Accounting.DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.DataLayer.Services
{
    public class CustomerRepository : ICustomersRepository
    {
        Accounting_DBEntities db = new Accounting_DBEntities();


        public List<Customers> getAllCustomers()
        {
            return db.Customers.ToList();
        }

        public Customers getCustomerById(int customerId)
        {
            return db.Customers.Find(customerId);
        }

        public bool insertCustomer(Customers customer)
        {
            try
            {
                db.Customers.Add(customer);
                return true;

            }
            catch
            {
                return false;
            }
        }

        public bool updateCustomer(Customers customer)
        {
            try
            {
                db.Entry(customer).State = EntityState.Modified;
                return true;

            }
            catch
            {
                return false;
            }
        }
        public bool deleteCustomer(Customers customer)
        {
            try
            {
                db.Entry(customer).State = EntityState.Deleted;
                return true;

            }
            catch
            {
                return false;
            }
        }

        public bool deleteCustomer(int customerId)
        {
            try
            {
                var customer = getCustomerById(customerId);
                deleteCustomer(customer);
                return true;

            }
            catch
            {
                return false;
            }
        }

        public void save()
        {
            db.SaveChanges();
        }


    }
}
