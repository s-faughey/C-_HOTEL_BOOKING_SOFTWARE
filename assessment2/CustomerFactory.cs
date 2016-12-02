using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assessment2
{
    class CustomerFactory
    {
        public Customer createCustomer(string name, string address, int customerReferenceNumber)
        {
            Customer customer = new Customer(name, address, customerReferenceNumber);
            return customer;
        }
    }
}
