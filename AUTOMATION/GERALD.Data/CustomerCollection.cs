using System;
using System.Collections.Generic;
using System.Linq;

namespace GERALD.Data
{
    public class CustomerCollection
    {
        private const string CUSTOMER_LIST_IS_EMPTY = "customer list is empty";
        private readonly List<Customer> Customers;
        private readonly Random random;

        public CustomerCollection()
        {
            Customers = new List<Customer>();
            random = new Random();
        }

        public void AddCustomer(Customer customer)
        {
            Customers.Add(customer);
        }

        public void Clear()
        {
            Customers.Clear();
        }

        public Customer GetRandomCustomer()
        {
            if (Customers.Count < 1)
            {
                throw new InvalidOperationException(CUSTOMER_LIST_IS_EMPTY);
            }
            int i = random.Next(Customers.Count());
            return Customers[i];
        }

    }
}
