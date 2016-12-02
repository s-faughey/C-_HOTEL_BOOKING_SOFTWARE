using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assessment2
{
    class GuestFactory
    {
        public Guest createGuest(string guestName, string passportNumber, int age)
        {
            Guest guest = new Guest(guestName, passportNumber, age);
            return guest;
        }
    }
}
