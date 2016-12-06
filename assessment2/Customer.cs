using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Author name: Sean Faughey
//Description: This is a customer object meant to represent a customer in the Napier Holiday Village
//  Holds information regarding the customer
//This class is hidden from the GUI using the facade design pattern (Facade class), and is created using the CustomerFactory class.
//Last modified: 06/12/2016
namespace assessment2
{
    [Serializable] //allows the customer to be added to a file using the serializer
    public class Customer
    {
        private string address; //represents the customer's address
        private int customerReferenceNumber;    //unique identifier of a customer
        private string name;    //represents the customer's name

        //constructor 

        public Customer(string name, string address, int customerReferenceNumber)
        {
            this.address = address;
            this.customerReferenceNumber = customerReferenceNumber;
            this.name = name;
        }

        //gets and setters of the private properties

        public int CustomerReferenceNumber {
            get
            {
                return this.customerReferenceNumber;
            }
            set
            {
                if (value == null) {
                    throw new Exception("This is wrong");
                }
                this.customerReferenceNumber = value;
            }
        }

        public string Address
        {
            get
            {
                return this.address;
            }
            set
            {
                if (value == null) {
                    throw new Exception("This is wrong.");
                }
                this.address = value;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (value == null) {
                    throw new Exception("This is wrong.");
                }
                this.name = value;
            }
        }
    }
}
