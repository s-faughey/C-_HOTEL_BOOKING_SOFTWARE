using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Author name: Sean Faughey
//Description: This is a Guest object meant to represent a guest in the Napier holiday Village
//  Holds information regarding the Guest
//This class is hidden from the GUI using the facade design pattern (Facade class), and is created using the GuestFactory class.
//Last modified: 03/12/2016
namespace assessment2
{
    [Serializable] //allows the guest to be written to file using the serializer
    public class Guest
    {
        private int age; //integer to represent the guest's age
        private string name; //represents the guest's name
        private string passportNumber; //unique identifier of the guest representing their passport number

        //constructor
        public Guest(string name, string passportNumber, int age)
        {
            this.age = age;
            this.name = name;
            this.passportNumber = passportNumber;
        }

        //getters and setters of private properties

        public string PassportNumber
        {
            get
            {
                return this.passportNumber;
            }
            set
            {
                if (value == null)
                {
                    throw new Exception("This is wrong");
                }
                this.passportNumber = value;
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
                    throw new Exception("This is wrong");
                }
                this.name = value;
            }
        }

        public int Age
        {
            get
            {
                return this.age;
            }
            set
            {
                if (value == null) {
                    throw new Exception("This is wrong");
                }
                this.age = value;
            }
        }
    }
}
