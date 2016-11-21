using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assessment2
{
    class GuestFactory
    {
        public Guest createGuest(string guestName, int passportNumber, int age)
        {
            Guest guest = new Guest(age, guestName, passportNumber);
            return guest;
        }

        private void deleteGuest()
        {

        }

        private void amendGuest()
        {

        }
    }
}
