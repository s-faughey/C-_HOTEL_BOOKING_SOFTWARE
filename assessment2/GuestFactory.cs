using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Author name: Sean Faughey
//Description: This is a factory which follows the factory design pattern in order to create guests with unique identifiers
//This class is hidden from the GUI using the facade design pattern (Facade class)
//Last modified: 06/12/2016
namespace assessment2
{
    class GuestFactory
    {
        public Guest createGuest(string guestName, string passportNumber, int age) //create a guests and return it
        {
            Guest guest = new Guest(guestName, passportNumber, age);
            return guest;
        }
    }
}
