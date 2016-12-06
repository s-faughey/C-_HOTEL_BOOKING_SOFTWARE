using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Author name: Sean Faughey
//Description: This is a factory which follows the factory design pattern in order to create Customers with unique identifiers
//This class is hidden from the GUI using the facade design pattern (Facade class)
//Last modified: 03/12/2016
namespace assessment2
{
    class CustomerFactory
    {

        //method to create a customer and return it
        public Customer createCustomer(string name, string address, int customerReferenceNumber)
        {
            Customer customer = new Customer(name, address, customerReferenceNumber);
            return customer;
        }
    }
}
