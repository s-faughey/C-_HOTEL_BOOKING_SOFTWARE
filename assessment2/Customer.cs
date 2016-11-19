using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assessment2
{
    [Serializable]
    class Customer
    {
        private string address;
        private int customerReferenceNumber;
        private string name;

        public Customer(string name, string address, int customerReferenceNumber)
        {
            this.address = address;
            this.customerReferenceNumber = customerReferenceNumber;
            this.name = name;
        }

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
                this.address = value;
            }
        }
    }
}
